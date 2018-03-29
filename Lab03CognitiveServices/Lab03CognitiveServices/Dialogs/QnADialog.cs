using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Dialogs
{
    [Serializable]
    public class QnADialog : QnAMakerDialog
    {

        // constructor de la clase
        public QnADialog() : base(new QnAMakerService(new QnAMakerAttribute(ConfigurationManager.AppSettings["QnASubcriptionkey"], ConfigurationManager.AppSettings["QnAKnowlegdebaseId"], "No se encuentra respuesta a la pregunta.", 0.5)))
        {

        }

        protected override async Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {

            var PrimerRespuesta = result.Answers.First().Answer;

            Activity Respuesta = ((Activity)context.Activity).CreateReply();

            var DatosRespuesta = PrimerRespuesta.Split(';');

            if (DatosRespuesta.Length == 1)
            {
                await context.PostAsync(PrimerRespuesta);
                return;
            }

            var Titulo = DatosRespuesta[0];
            var Descripcion = DatosRespuesta[1];
            var URL = DatosRespuesta[2];
            var URLImagen = DatosRespuesta[3];

            HeroCard Card = new HeroCard
            {
                Title = Titulo,
                Text = Descripcion
            };

            Card.Buttons = new List<CardAction>
            {
                new CardAction(ActionTypes.OpenUrl, "Diplomado Bots", value:URL)
            };

            Card.Images = new List<CardImage>
            {
                new CardImage(url:URLImagen)
            };

            Respuesta.Attachments.Add(Card.ToAttachment());

            await context.PostAsync(Respuesta);

        }

    }
}
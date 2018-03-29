using Microsoft.Bot.Connector;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using System.Net.Http;
using System;

namespace HelloBotLab02.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {

        /// <summary>
        /// El método Conversation.SendAsync es clave para para implementar diálogos con el BotBuilder SDK for .NET.
        /// Este método es una máquina de estado que sigue el principio de Inversión de dependenciasy realiza los siguientes pasos:
        /// 1.Crea instancias de los componentes requeridos.
        /// 2.Deserializa el estado de la conversación (la pila de diálogos y el estado de cada dialogo en la pila) desde el IBotDataStore. 
        /// 3. Reanuda el proceso de conversación donde fue suspendido y espera un mensaje. 
        /// 4. Envía las respuestas. 5. Serializa el estado actualizado de la c onversación y lo guarda de regreso en el IBotDataStore
        /// Cuando la conversación da inicio, el diálogo no contiene estado, por lo tanto Conversation.SendAsync crea una instancia de la clase que implementa
        /// IDialog (MyDialog) e invoca a su método StartAsync.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if(activity.Type == ActivityTypes.Message)
            {
                // conectar mi logica de manejo de mensajes cuando la actividad sea de message
                await Conversation.SendAsync(activity, () => new Dialogs.MyDialog());
            } else
            {
                HandleSystemMessage(activity);
            }

            // Devolver la respuesta del método Post
            var Response = Request.CreateResponse(System.Net.HttpStatusCode.OK);
            return Response;

        }

        /// <summary>
        /// Manejar otros objetos del tipo activity
        /// ConversationUpdate = Implemente IConversationUpdateActivity.  El bot fue agregado a la conversación, se agrego un nuevo miembro, se eliminó, o los metadatos cambiaron.
        /// ContactRelationUpdate = Implementa IContactRelationUpdateActivity . El bot fue agregado o elminado de la lista de contactos de un usuario.
        /// Typing = Implenta ITypingActivity. El usuario o bot esta escribiendo una respuesta.
        /// Ping = Intento para determinar sin un endpoint del bot es accesible
        /// DeleteUserData = Indica que un usuario a solicitado que el bot elimine cualquier datos del usuario que pueda haber almacenado
        /// EndOfConversation  = Implementa IEndOfConversationActivity. Indica el final de la conversación.
        /// Event = Implementa IEventActivity. Representa una comunicación enviada al bot que no es visible por el usuario.
        /// Invoke =  Implementa IInvokeActivity. Se utiliza para indicar al bot realice una operación. Esta reservado para uso del Bot Framework.
        /// MessageReaction = Implementa IMessageReactionActivity. Un usuario reacciono a alguna actividad existente.
        /// </summary>
        /// <param name="activity"></param>
        private void HandleSystemMessage(Activity message)
        {
            switch(message.Type)
            {
                case ActivityTypes.ConversationUpdate:
                    break;
                case ActivityTypes.ContactRelationUpdate:
                    break;
                case ActivityTypes.Typing:
                    break;
                case ActivityTypes.Ping:
                    break;
                case ActivityTypes.DeleteUserData:
                    break;
            }
        }
    }
}

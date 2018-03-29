using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;


namespace Dialogs
{
    /// <summary>
    /// El estado del diálogo es salvado automáticamente por el Bot Framework y, en consecuencia, el Framework indica que los objetos IDialog deben ser serializables.
    /// Sin este atributo, el Framework no podría guardar el estado del diálogo en el almacén de estado
    /// </summary>
    [System.Serializable]
    public class MyDialog : IDialog<string> // Cada bot puede devolver un valor del tipo especificado como parámetro del tipo genérico. 
    {

        /// <summary>
        /// El método recibe  un objeto IDialogContext que será utilizado para enviar mensajes al usuario, suspender el diálogo actual, navegar a otro diálogo y , 
        /// almacenar y recuperar el estado
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task StartAsync(IDialogContext context)
        {
            /**
             *  El método  Wait del objeto  IDialogContext suspende la ejecución actual, guarda el estado y  espera entradas del usuario.
             *  Tan pronto recibe una entrada del usuario,  este ejecuta e l  método que le es proporcionado como parámetro  de tipo Resume After<T> Delegate suspendiendo la ejecución actual
             */
            context.Wait(MessageReceiveAsync);
        }

        //public delegate TaskResumeAfter<inT>(IDialogContext context, IAwaitable<in T> result); -> asi funciona el delegado para la funcion

        /// <summary>
        /// La implementación del método MessageReceivedAsync es el corazón de la clase MyDialog
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private async Task MessageReceiveAsync(IDialogContext context, IAwaitable<object> result)
        {
            // El Bot Framework Connector utiliza un objeto Activity para pasar información de ida y vuelta entre el bot y el canal(usuario).
            var ActivityMessage = await result as Activity;
            if (int.TryParse(ActivityMessage.Text, out int Number)) // obtener mensaje enviado por el usuario y trato de convertirlo a int.
                await context.PostAsync(Number % 2 == 0 ? "El valor ingresado es Par." : "El valor ingresado es Impar.");
            else
                await context.PostAsync("Debes de ingresar un valor numerico para determinar si es par o impar.");
            
            // Para esperar el siguiente mensaje
            context.Wait(MessageReceiveAsync);

        }
    }
}
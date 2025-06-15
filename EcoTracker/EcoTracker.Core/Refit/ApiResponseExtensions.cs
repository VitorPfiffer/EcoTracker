using Refit;

namespace EcoTracker.Core.Refit
{
    public static class ApiResponseExtensions
    {
        /// <summary>
        /// Verifica se a resposta da API foi bem-sucedida.
        /// Considera a resposta como sucesso se ela contém conteúdo, se o resultado da operação não é nulo, e se não há notificações de erro.
        /// </summary>
        /// <typeparam name="T">O tipo de conteúdo contido na resposta da API.</typeparam>
        /// <param name="apiResponse">A resposta da API a ser verificada.</param>
        /// <returns>
        /// Retorna true se a resposta contém conteúdo, o resultado não é nulo, e não há notificações de erro;
        /// caso contrário, retorna false.
        /// </returns>
        public static bool IsSucess<T>(this ApiResponse<ApplicationResult<T>> apiResponse)
        {
            //Se tiver content e result é sucesso
            return apiResponse.HasContent() && apiResponse.Content.Result != null;
        }

        public static IEnumerable<string> GetErrors<T>(this ApiResponse<ApplicationResult<T>> apiResponse)
        {
            var errors = apiResponse.Content.Notifications.Select(x => x.Message).ToList();

            return errors;
        }

        public static bool HasContent<T>(this ApiResponse<ApplicationResult<T>> apiResponse)
        {
            return apiResponse.Content != null;
        }


    }

}

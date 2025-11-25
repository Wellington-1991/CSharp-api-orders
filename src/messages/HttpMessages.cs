namespace api_orders.Helpers;

public static class HttpMessages
{
    public static class Success
    {
        public const string CREATED = "Criado com sucesso.";
        public const string ACCEPTED = "Apontamento realizado com sucesso. ALERTA: O tempo de ciclo informado é menor que o cadastrado."; 
        public const string OK = "Requisição bem-sucedida.";
        public const string NO_CONTENT = "Nenhum conteúdo para exibir.";
        public const string DELETED = "Deletado com sucesso.";
    }

    public static class Error
    {
        public const string BAD_REQUEST = "Requisição inválida.";
        public const string UNAUTHORIZED = "Não autorizado.";
        public const string DATE_OUT_OF_RANGE_ERROR = "Falha no apontamento - Data do apontamento fora do período permitido para o usuário.";
        public const string FORBIDDEN = "Acesso proibido.";
        public const string NOT_FOUND = "Recurso não encontrado.";
        public const string CONFLICT = "Conflito de recurso.";
        public const string GONE = "Recurso removido permanentemente.";
        public const string PRECONDITION_FAILED = "Falha na pré-condição.";
        public const string TOO_MANY_REQUESTS = "Muitas requisições.";
        public const string INTERNAL_SERVER_ERROR = "Erro interno do servidor.";
        public const string SERVICE_UNAVAILABLE = "Serviço indisponível.";
    }
}

using Google.Cloud.Firestore;

namespace gisa.mic.backend.Model
{
    [FirestoreData]
    public class Associado
    {
        public string Id { get; set; }

        [FirestoreProperty]
        public string Nome { get; set; }

        [FirestoreProperty]
        public string Descricao { get; set; }

        [FirestoreProperty]
        public string Cpf { get; set; }

        [FirestoreProperty]
        public string Rg { get; set; }

        [FirestoreProperty]
        public string Sexo { get; set; }

        [FirestoreProperty]
        public string Cep { get; set; }

        [FirestoreProperty]
        public string Endereco { get; set; }

        [FirestoreProperty]
        public string Numero { get; set; }

        [FirestoreProperty]
        public string Bairro { get; set; }

        [FirestoreProperty]
        public string Cidade { get; set; }

        [FirestoreProperty]
        public string UF { get; set; }

        [FirestoreProperty]
        public string Pais { get; set; }

        [FirestoreProperty]
        public string Contato { get; set; }

        [FirestoreProperty]
        public string InformacoesAdicionais { get; set; }

        [FirestoreProperty]
        public bool Ativo { get; set; }

    }
}

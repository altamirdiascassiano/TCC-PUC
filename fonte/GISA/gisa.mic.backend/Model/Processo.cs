using Google.Cloud.Firestore;
using System;

namespace gisa.mic.backend.Model
{
    [FirestoreData]
    public class Processo
    {
        public string Id{ get; set; }
        
        [FirestoreProperty]
        public string NomePrestador{ get; set; }

        [FirestoreProperty]
        public string NomeAssociado{ get; set; }

        [FirestoreProperty]
        public string IdPrestador { get; set; }

        [FirestoreProperty]
        public string IdAssociado { get; set; }

        [FirestoreProperty]
        public string DescricaoProcesso { get; set; }

        [FirestoreProperty]
        public string StatusProcesso { get; set; }

    }
    enum StatusProcesso
    {
        EmAnalise,
        EmExecucao,
        EmProcessoContabil,
        Finalizado,
        Cancelado
    }
}

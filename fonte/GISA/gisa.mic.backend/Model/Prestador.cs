using Google.Cloud.Firestore;
using System;

namespace gisa.mic.backend.Model
{
    [FirestoreData]
    public class Prestador
    {
        public string Id{ get; set; }

        //[FirestoreProperty]
        //public DateTime DtCadastro { get; set; }

        [FirestoreProperty]
        public string Nome { get; set; }

        [FirestoreProperty]
        public string Descricao { get; set; }
        
    }
}
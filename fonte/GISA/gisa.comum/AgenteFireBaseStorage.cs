using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gisa.comum
{
    public class AgenteFireBaseStorage
    {
        private string diretorio = "/app/bin/Debug/net5.0/gisa-c54d2-77dcdf90ba57.json";
        private string projetoId;
        private FirestoreDb _firestoreDb;
        public AgenteFireBaseStorage()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", diretorio);
            projetoId = "gisa-c54d2";
            _firestoreDb = FirestoreDb.Create(projetoId);
        }

        public async Task<List<string>> BuscaTodosDadosColecaoAsync(string nomeColecao)
        {           
            Query query = _firestoreDb.Collection(nomeColecao);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            List<string> listaDocumentos= new List<string>();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> docDictionary = documentSnapshot.ToDictionary();
                    //Ver como resolver pois está dando problema na conversão da data do firebase com o C#
                    docDictionary["DtCadastro"] = DateTime.Now.ToString();
                    listaDocumentos.Add(JsonConvert.SerializeObject(docDictionary));
                }
            }
            return listaDocumentos;
        }
    }
}

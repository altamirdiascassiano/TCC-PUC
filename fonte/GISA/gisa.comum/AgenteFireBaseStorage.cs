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

        public async Task<List<T>> BuscaTodosDocumentosColecaoAsync<T>(string nomeColecao)
        {           
            Query query = _firestoreDb.Collection(nomeColecao);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            List<T> listaDocumentos= new List<T>();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> docDictionary = documentSnapshot.ToDictionary();
                    docDictionary.Add("Id", documentSnapshot.Id);
                    docDictionary["DtCadastro"] = DateTime.Now.ToString(); //Tem resolver problema de conversão da data
                    string json = JsonConvert.SerializeObject(docDictionary);                    
                    T novoDoc = JsonConvert.DeserializeObject<T>(json);
                    listaDocumentos.Add(novoDoc);

                }
            }
            return listaDocumentos;
        }

        public async Task<bool> AdicionaDocumentoNaColecao<T>(string nomeColecao, T documento)
        {
            try
            {
                CollectionReference referenciaColecao = _firestoreDb.Collection(nomeColecao);
                await referenciaColecao.AddAsync(documento);
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

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

        public async Task<T> BuscaDocumentoColecaoPorIdAsync<T>(string nomeColecao, string idDocumento)
        {          
            DocumentReference documentReference = _firestoreDb.Collection(nomeColecao).Document(idDocumento);
            DocumentSnapshot documentSnapshot = await documentReference.GetSnapshotAsync();

            if (documentSnapshot.Exists)
            {
                Dictionary<string, object> docRecuperado = documentSnapshot.ToDictionary();
                docRecuperado.Add("Id", documentSnapshot.Id);
                string json = JsonConvert.SerializeObject(docRecuperado);
                T novoDoc = JsonConvert.DeserializeObject<T>(json);
                return novoDoc;
            }

            return default(T);
        }

        public async Task<List<T>> BuscaTodosDocumentosColecaoAsync<T>(string nomeColecao)
        {
            Query query = _firestoreDb.Collection(nomeColecao);
            QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
            List<T> listaDocumentos = new List<T>();

            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> docDictionary = documentSnapshot.ToDictionary();
                    docDictionary.Add("Id", documentSnapshot.Id);               
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveDocumentoNaColecao<T>(string nomeColecao, string idDocumento)
        {
            try
            {
                DocumentReference referenciaDocumento = _firestoreDb.Collection(nomeColecao).Document(idDocumento);
                await referenciaDocumento.DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AtualizaDocumentoNaColecao<T>(string nomeColecao, string idDocumento, T entidadeComAtualizacao)
        {
            try
            {
                DocumentReference referenciaDocumento = _firestoreDb.Collection(nomeColecao).Document(idDocumento);
                await referenciaDocumento.SetAsync(entidadeComAtualizacao, SetOptions.Overwrite);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

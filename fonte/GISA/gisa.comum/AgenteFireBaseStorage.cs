using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gisa.comum
{
    public class AgenteFireBaseStorage
    {                
        private FirestoreDb _firestoreDb;

        public AgenteFireBaseStorage(string projetoId, string firebaseStorageJsonAuth)
        {
            try
            {
                var builder = new FirestoreClientBuilder { JsonCredentials = firebaseStorageJsonAuth };
                _firestoreDb = FirestoreDb.Create(projetoId, builder.Build());
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw ex;
            }
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

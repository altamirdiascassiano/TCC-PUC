/* eslint-disable react/jsx-props-no-spreading */
/*eslint-disable react/no-array-index-key */
/*eslint prefer-const: 0*/
import React, { useState, useEffect, forwardRef } from "react";
import moment from "moment";
import 'moment/locale/pt-br';
import { useHistory, Link } from 'react-router-dom';
import { Breadcrumbs, Paper, useSnackbar } from "framework-ui";
import MaterialTable, { Icons } from "material-table";
import { Home } from "@material-ui/icons";
import AddBox from '@material-ui/icons/AddBox';
import ArrowUpward from '@material-ui/icons/ArrowDownward';
import Check from '@material-ui/icons/Check';
import ChevronLeft from '@material-ui/icons/ChevronLeft';
import ChevronRight from '@material-ui/icons/ChevronRight';
import Clear from '@material-ui/icons/Clear';
import DeleteOutline from '@material-ui/icons/DeleteOutline';
import Edit from '@material-ui/icons/Edit';
import FilterList from '@material-ui/icons/FilterList';
import FirstPage from '@material-ui/icons/FirstPage';
import LastPage from '@material-ui/icons/LastPage';
import Remove from '@material-ui/icons/Remove';
import SaveAlt from '@material-ui/icons/SaveAlt';
import Search from '@material-ui/icons/Search';
import ViewColumn from '@material-ui/icons/ViewColumn';
import MedicationIcon from '@mui/icons-material/Medication';
import api from "../../services/api";
import { ApiPrestador } from "../../services/base"

const tableIcons : Icons = {
  Add: forwardRef((props, ref) => <AddBox {...props} ref={ref} />),
  Check: forwardRef((props, ref) => <Check {...props} ref={ref} />),
  Clear: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
  Delete: forwardRef((props, ref) => <DeleteOutline {...props} ref={ref} />),
  DetailPanel: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
  Edit: forwardRef((props, ref) => <Edit {...props} ref={ref} />),
  Export: forwardRef((props, ref) => <SaveAlt {...props} ref={ref} />),
  Filter: forwardRef((props, ref) => <FilterList {...props} ref={ref} />),
  FirstPage: forwardRef((props, ref) => <FirstPage {...props} ref={ref} />),
  LastPage: forwardRef((props, ref) => <LastPage {...props} ref={ref} />),
  NextPage: forwardRef((props, ref) => <ChevronRight {...props} ref={ref} />),
  PreviousPage: forwardRef((props, ref) => <ChevronLeft {...props} ref={ref} />),
  ResetSearch: forwardRef((props, ref) => <Clear {...props} ref={ref} />),
  Search: forwardRef((props, ref) => <Search {...props} ref={ref} />),
  SortArrow: forwardRef((props, ref) => <ArrowUpward {...props} ref={ref} />),
  ThirdStateCheck: forwardRef((props, ref) => <Remove {...props} ref={ref} />),
  ViewColumn: forwardRef((props, ref) => <ViewColumn {...props} ref={ref} />)
};

const Prestadores = () => {  
  const [dados, setDados] = useState<any>([]);
  const [isFetching, setIsFetching] = useState<boolean>(true);
  const { message } = useSnackbar(); 
  let history = useHistory();

  async function fetchPrestadores() {
    let _i;
    const Dados: any[] = [];

    sessionStorage.clear();

    const result = await api(`${ApiPrestador}`, 'GET');
    if (result.error) {
      console.log(result.error) // eslint-disable-line no-console
    } else {
      let data = JSON.parse(result);
      const Jsondata = () => { 
        for (_i = 0; _i < data.length; _i += 1) {
            Dados.push({ 
              id: data[_i].id, 
              nome: data[_i].nome,
              cpf: data[_i].cpf,
              identificadorCarteiraProfissional: data[_i].identificadorCarteiraProfissional,
              descricao: data[_i].descricao,
              rg: data[_i].rg,
              sexo: data[_i].sexo,
              cep: data[_i].cep,
              endereco: data[_i].endereco,
              numero: data[_i].numero,
              bairro: data[_i].bairro,
              cidade: data[_i].cidade,
              uf: data[_i].uf,
              contato: data[_i].contato,
              informacoesAdicionais: data[_i].informacoesAdicionais
            })
        }
        return Dados;
      }
      setDados(Jsondata);      
    }
    setIsFetching(false);
  }

  const handleCreate = () => {    
    sessionStorage.clear();
    history.push("/prestadores/prestador");
  }

  const handleDelete = async (rowData: any) => {
    setIsFetching(true);
    const result = await api(`${ApiPrestador}/${rowData.id}`, 'DELETE');
    if (result.error) {
      console.log(result.error) // eslint-disable-line no-console
      message.error('Algo deu errado :(');
    } else {
      message.success('Excluído!')
      const newDados = dados.filter((item: any) => item.id !== rowData.id);
      setDados(newDados);
    } 
    setIsFetching(false);
  }

  const handleEdit = (rowData: any) => {
    sessionStorage.setItem("rowDataPrestador", JSON.stringify(rowData));
    history.push("/prestadores/prestador");
  }

  useEffect(() => {       
    fetchPrestadores();
  }, [])

  return (    
    <div style={{ maxWidth: 1500, margin: "0 auto", paddingTop: 32, position: "relative" }}>
      <div style={{ display: "flex", alignItems: "center", justifyContent: "space-between" }}>
        <h3 style={{ margin: 0, color: "red" }}>Prestadores do Boa Saúde</h3>
        <Breadcrumbs
          items={[
            { title: "Home", as: Link, props: { to: "/home" }, icon: Home },
            { title: "Prestadores", icon: MedicationIcon }
          ]}
        />
      </div>
      
      <Paper elevation={10} style={{ marginTop: 24 }}>        
        <MaterialTable
          title='Prestadores Cadastrados'
          icons={tableIcons}   
          isLoading={isFetching}   
          columns={[
            { title: 'ID', field: 'id', searchable: true, hidden: true },
            { title: 'Nome', field: 'nome', searchable: true},
            { title: 'CPF/CNPJ', field: 'cpf', searchable: true},
            { title: 'Descrição', field: 'descricao', searchable: true},
            { title: 'Identificador Carteira Profissional', field: 'identificadorCarteiraProfissional', searchable: true },  
            { title: 'contato', field: 'contato', hidden: true },  
            { title: 'RG', field: 'rg', hidden: true },     
            { title: 'Sexo', field: 'sexo', hidden: true },  
            { title: 'Endereco', field: 'endereco', hidden: true },  
            { title: 'Numero', field: 'numero', hidden: true },  
            { title: 'Bairro', field: 'bairro', hidden: true },  
            { title: 'Cidade', field: 'cidade', hidden: true },  
            { title: 'CEP', field: 'cep', hidden: true },  
            { title: 'UF', field: 'uf', hidden: true },  
            { title: 'Informacoes Adicionais', field: 'informacoesAdicionais', hidden: true },       
          ]}
          localization={{
            body: {
              emptyDataSourceMessage: ''
            },
            toolbar: {
              searchTooltip: 'Pesquisar',
              searchPlaceholder: 'Nome , Cpf, ...',
              nRowsSelected: '{0} linhas(s) selecionadas'              
            },
            grouping: {
              groupedBy: 'Agrupado por',
              placeholder: "Arraste colunas aqui para visualização por agrupamento"
            },
            header: {
              actions: 'Ações'              
            },
            pagination: {
              labelRowsSelect: 'linhas por página',
              labelDisplayedRows: '{count} de {from}-{to}',
              firstTooltip: 'Primeira página',
              previousTooltip: 'Página anterior',
              nextTooltip: 'Próxima página',
              lastTooltip: 'Última página'
            }
          }}
          data={dados} 

          actions={[
            rowData => ({
              icon: Edit,              
              tooltip: 'Editar',
              onClick: () => handleEdit(rowData)              
            }),
            rowData => ({
              icon: () => (<DeleteOutline style={{ color: "red" }} />),
              tooltip: 'Excluir',
              onClick: () => handleDelete(rowData)              
            }),            
            {
              icon: () => (<AddBox style={{ color: "red" }} />),
              tooltip: 'Cadastrar Prestador',
              isFreeAction: true,
              onClick: handleCreate
            }
          ]}

          options={{
            actionsColumnIndex: -1,
            grouping: false,   
            headerStyle: { position: 'static', top: 0, backgroundColor: "#d8d8d8"}, maxBodyHeight: '3000px',
            selection: false,
            selectionProps: (rowData: any) => ({
              disabled: moment(rowData.DateStart).format() < moment(Date.now()).format(),
              color: 'primary'
            })
          }}
        />  
      </Paper>
    </div>
  );
};

export default Prestadores;

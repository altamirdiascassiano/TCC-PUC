/* eslint-disable react/jsx-props-no-spreading */
/*eslint-disable react/no-array-index-key */
/*eslint prefer-const: 0*/
import React, { useState, useEffect, forwardRef } from "react";
import moment from "moment";
import 'moment/locale/pt-br';
import { useHistory, Link } from 'react-router-dom';
import { Breadcrumbs, Button, Paper, useSnackbar } from "framework-ui";
import MaterialTable, { Icons } from "material-table";
import { Home, Schedule } from "@material-ui/icons";
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
      console.log(result.error)
    } else {
      let data = JSON.parse(result);
      const Jsondata = () => { 
        for (_i = 0; _i < data.length; _i += 1) {
            Dados.push({ 
              id: data[_i].id, 
              Nome: data[_i].nome,
              CPF: data[_i].cpf,
              CRM: data[_i].identificadorCarteiraProfissional
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
      console.log(result.error)
      message.error('Algo deu errado :(');
    } else {
      message.success('Excluído!')
      const newDados = dados.filter((item: any) => item.id !== rowData.id);
      setDados(newDados);
    } 
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
            { title: "Prestadores", icon: Schedule }
          ]}
        />
      </div>
      
      <Paper elevation={10} style={{ marginTop: 24 }}>        
        <MaterialTable
          title='Prestadores Cadastrados'
          icons={tableIcons}   
          isLoading={isFetching}   
          columns={[
            { title: 'ID', field: 'id' },
            { title: 'Nome', field: 'Nome'},
            { title: 'CPF', field: 'CPF'},
            { title: 'CRM', field: 'CRM', searchable: true },            
          ]}
          localization={{
            body: {
              emptyDataSourceMessage: 'Nenhum registro para exibir - Tente outro período!'
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

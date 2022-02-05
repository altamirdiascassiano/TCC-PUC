import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import { Paper, Button, Breadcrumbs, Tabs, useSnackbar } from "framework-ui";
import { Assignment, Home } from "@material-ui/icons";
import { createStyles, makeStyles } from '@material-ui/core/styles';
import { green } from "@material-ui/core/colors";
import 'moment/locale/pt-br';
import CircularProgress from '@material-ui/core/CircularProgress';
import FormRegister from './FormRegister';
import ColorsDefault from "../../../utils/colors";
import api from "../../../services/api";
import { ApiProcesso } from "../../../services/base"

const useStyles = makeStyles(() =>
  createStyles({
    buttonProgress: {
      color: green[500],
      position: 'absolute',
      top: '50%',
      left: '50%',
      marginTop: -12,
      marginLeft: -12,
    },
  }),
);

const AddProcesso = () => {
  const datalocation = sessionStorage.getItem('rowDataProcesso');  
  const history = useHistory();
  const { message } = useSnackbar();  
  const Title = "Cadastro de Processo";
  const [values, setValues] = useState<any>(datalocation ? JSON.parse(datalocation || '') : undefined);
  const [currentTab, setCurrentTab] = useState(0);
  const [loading, setLoading] = React.useState(false);  
  const required = ["nomePrestador", "nomeAssociado", "descricaoProcesso"]; 
  const classes = useStyles();
  const handleTabChange: any = (_: any, tab: any) => {
    setCurrentTab(tab);
  }; 

  const handleChange = ( component: any) => {
    if (component.target) {
      if (component.target.type === "checkbox") setValues({ ...values, [component.target.name]: component.target.checked });
      if (component.target.type === "radio") setValues({ ...values, [component.target.name]: component.target.checked });
      if (component.target.type === 'text') setValues({ ...values, [component.target.name]: component.target.value })
      if (component.target.type === 'textarea') setValues({ ...values, [component.target.name]: component.target.value })
      if (component.target.type === 'datetime-local') setValues({ ...values, [component.target.id]: component.target.value })
      if (component.target.type === undefined) setValues({ ...values, [component.target.name]: component.target.value })     
    } 
  };

  function wait(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  const handleSubmit = async () => { 
    if (!loading) {
      setLoading(true);      
    }  
    
    let result;

    if (values.id === "" ) 
    {
      result = await api(`${ApiProcesso}`, 'POST', JSON.stringify(values));
    }  
    else 
    {
      result = await api(`${ApiProcesso}/${values.id}`, 'PUT', JSON.stringify(values));
    }

    if (result.error) {
      console.log(result.error) // eslint-disable-line no-console
      message.error('Algo deu errado :(');
      setLoading(false);
    } else {
      message.success('Salvo com sucesso!')   
      sessionStorage.clear();
      wait(2000).then(() => { history.push("/Processos"); });
    } 
  };    

  const isValid = () => {
    try {
      return required.every((key) => values[key]);
    } catch {
      return false;
    }
    
  };

  return (    
    <div style={{ maxWidth: 1130, margin: "0 auto", paddingTop: 32, position: "relative" }}>
      <div style={{ display: "flex", alignItems: "center", justifyContent: "space-between" }}>
        <h3 style={{ margin: 0, color: ColorsDefault.primary }}>{Title}</h3>
        <Breadcrumbs
          items={[
            { title: "Home", as: Link, props: { to: "/home" }, icon: Home },
            { title: "Processos", as: Link, props: { to: "/Processos" }, icon: Assignment },
            { title: Title }
          ]}
        />
      </div>
      <Paper elevation={10} style={{ marginTop: 32 }}>
        <Tabs
          centered
          value={currentTab}
          indicatorColor="primary"
          onChange={handleTabChange}
          style={{ borderBottom: "1px solid #e2e2e2" }}
          items={[
            { label: "Identificação" }
          ]}
        />
        <form style={{ padding: 24 }}>
          {
          currentTab === 0 
          && 
          <FormRegister 
            values={values} 
            handleChange={handleChange} 
          />}
        </form>
      </Paper>
      <div style={{ marginTop: 24, display: "flex", justifyContent: "flex-end" }}>
        
        <Link style={{ textDecoration: "none" }} to="/Processos">
          {!loading && <Button style={{ marginRight: 8, color: "#FFF", background: ColorsDefault.error }} disableElevation>Cancelar</Button>}
        </Link>
        {currentTab === 0 && (
          <Button
            className="SaveButton"
            color="primary"
            disableElevation
            disabled={!isValid() || loading}
            onClick={handleSubmit}
          >
          Salvar
          {loading && <CircularProgress size={24} className={classes.buttonProgress} />}
          </Button>          
        )}
      </div>
    </div>
  );
};

export default AddProcesso;

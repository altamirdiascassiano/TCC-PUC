import React from "react";
import { Grid, TextField} from "framework-ui";

const FormRegister = ({ values, handleChange }: any) => {  

  return (
    <Grid container spacing={2}>
      <Grid item xs={12} md={12}>
        <Grid item xs={12} md={3}>
          <TextField
            required
            name="id"
            label="ID"
            onChange={handleChange}
            value={values ? values.id : ""}
            disabled
            InputLabelProps={{
              shrink: true,
            }}
          />
        </Grid>   
      </Grid>
      <Grid item xs={6} md={3}>
        <TextField
          name="idPrestador"
          label="ID Prestador"
          onChange={handleChange}
          value={values ? values.idPrestador : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>  
      <Grid item xs={6} md={9}>
        <TextField
          required
          name="nomePrestador"
          label="Nome do Prestador"
          onChange={handleChange}
          value={values ? values.nomePrestador : ""}
          InputLabelProps={{
            shrink: true,
          }}
          autoFocus
        />
      </Grid>
      <Grid item xs={6} md={3}>
        <TextField
          name="idAssociado"
          label="ID Associado"
          onChange={handleChange}
          value={values ? values.idAssociado : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>  
      <Grid item xs={6} md={9}>
        <TextField
          required
          name="nomeAssociado"
          label="Nome do Associado"
          onChange={handleChange}
          value={values ? values.nomeAssociado : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>    
      <Grid item xs={6} md={9}>
        <TextField
          required
          name="statusProcesso"
          label="Status"
          onChange={handleChange}
          value={values ? values.statusProcesso : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid> 

      <Grid item xs={12} md={12}>
        <TextField
          name="descricaoProcesso"
          label="Descrição do Procsso"
          onChange={handleChange}
          value={values ? values.descricaoProcesso : ""}
          InputLabelProps={{
            shrink: true,
          }}
          multiline
        />
      </Grid>        
    </Grid>
  );
};

export default FormRegister;

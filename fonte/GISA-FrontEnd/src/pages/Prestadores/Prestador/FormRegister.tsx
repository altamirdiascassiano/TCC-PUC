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
      <Grid item xs={12} md={6}>
        <TextField
          required
          name="nome"
          label="Nome"
          onChange={handleChange}
          value={values ? values.nome : ""}
          InputLabelProps={{
            shrink: true,
          }}
          autoFocus
        />
      </Grid>
      <Grid item xs={12} md={3}>
        <TextField
          required
          name="cpf"
          label="CPF"
          onChange={handleChange}
          value={values ? values.cpf : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>     
      <Grid item xs={12} md={12}>
        <TextField
          name="descricao"
          label="Descricao"
          onChange={handleChange}
          value={values ? values.descricao : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>       

      <Grid item xs={12} md={2}>
        <TextField
          name="rg"
          label="RG"
          onChange={handleChange}
          value={values ? values.rg : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>     

      <Grid item xs={12} md={3}>
        <TextField
          name="sexo"
          label="Sexo"
          onChange={handleChange}
          value={values ? values.sexo : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>     

      <Grid item xs={12} md={3}>
        <TextField
          name="identificadorCarteiraProfissional"
          label="Identificador Carteira Profissional"
          onChange={handleChange}
          value={values ? values.identificadorCarteiraProfissional : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>     

      <Grid item xs={12} md={6}>
        <TextField
          name="endereco"
          label="Endere??o"
          onChange={handleChange}
          value={values ? values.endereco : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>     

      <Grid item xs={12} md={1}>
        <TextField
          name="numero"
          label="N??mero"
          onChange={handleChange}
          value={values ? values.numero : ""}
          InputLabelProps={{
            shrink: true,
          }}
          
        />
      </Grid>   

      <Grid item xs={12} md={6}>
        <TextField
          name="bairro"
          label="Bairro"
          onChange={handleChange}
          value={values ? values.bairro : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>    

      <Grid item xs={12} md={4}>
        <TextField
          name="cidade"
          label="Cidade"
          onChange={handleChange}
          value={values ? values.cidade : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>   

      <Grid item xs={12} md={1}>
        <TextField
          name="uf"
          label="UF"
          onChange={handleChange}
          value={values ? values.uf : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>   

      <Grid item xs={12} md={2}>
        <TextField
          name="cep"
          label="CEP"
          onChange={handleChange}
          value={values ? values.cep : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>  

      <Grid item xs={12} md={8}>
        <TextField
          name="contato"
          label="Contato"
          onChange={handleChange}
          value={values ? values.contato : ""}
          InputLabelProps={{
            shrink: true,
          }}
        />
      </Grid>  

      <Grid item xs={12} md={12}>
        <TextField
          name="informacoesAdicionais"
          label="Informacoes Adicionais"
          onChange={handleChange}
          value={values ? values.informacoesAdicionais : ""}
          InputLabelProps={{
            shrink: true,
          }}
          multiline
        />
      </Grid>  
      {/* "id": "string",
      "contato": "string",
      "informacoesAdicionais": "string",

      {/* <Grid item xs={12} md={6}>
        <RadioField
          name="sexo"
          label="Sexo"
          color="primary"
          onChange={handleChange}
          value={values.checked}
          items={[
            { value: "M", label: "Masculino", color: "primary" },
            { value: "F", label: "Feminino", color: "primary" }
          ]}
        />
      </Grid>      */}
    </Grid>
  );
};

export default FormRegister;

/* eslint-disable react/jsx-props-no-spreading */
import React, { useEffect, useState } from "react";
import { Route, Switch, Redirect } from "react-router-dom";
import Home from "../pages/Home";

import Prestadores from "../pages/Prestadores";
import NovoPrestador from "../pages/Prestadores/Prestador/index";

import Associados from "../pages/Associados";
import NovoAssociado from "../pages/Associados/Associado/index";

import Processos from "../pages/Processos";
import NovoProcesso from "../pages/Processos/Processo/index";

const Routes = () => {

  const [auth] = useState<boolean>(true);

  useEffect(() => {
    // 
  }, [])
  
  const PrivateRoute = ({ component: Component, ...restProps }: any) => {
    return (    
      <Route
        {...restProps}
          render={(props) => {
            return (auth ? <Component {...props} /> : <Redirect to='/unauthorized'/>)             
          }
        }
      />
    )
  };

  return ( 
    <Switch> 
      <PrivateRoute exact path="/home" component={Home} />      
      <PrivateRoute exact path="/prestadores" component={Prestadores} />
      <PrivateRoute exact path="/prestadores/prestador" component={NovoPrestador} /> 
      <PrivateRoute exact path="/associados" component={Associados} />
      <PrivateRoute exact path="/associados/associado" component={NovoAssociado} /> 
      <PrivateRoute exact path="/processos" component={Processos} />
      <PrivateRoute exact path="/processos/processo" component={NovoProcesso} /> 
      <Route path="/*">  
        <Redirect to="/home"/>  
      </Route> 
    </Switch>  
  )
};

export default Routes;
/* eslint-disable react/jsx-props-no-spreading */
import React, { useEffect, useState } from "react";
import { Route, Switch, Redirect, useHistory } from "react-router-dom";
import googleHandler from "./auth";
import Home from "../pages/Home";
import Prestadores from "../pages/ListaPrestadores";
import NovoPrestador from "../pages/Prestador/index";

const Routes = () => {

  const history = useHistory();
  const [auth, setAuth] = useState<boolean>(true);

  useEffect(() => {
    // googleHandler().then(
    //   ({result, UserName}: any) => {
    //     let i;
    //     if (!UserName) {
    //       history.push('/')
    //       return; 
    //     }
    //     if (result.error) {
    //       console.log(result);                    
    //     } else {
    //       for (i = 0; i < result[UserName].roles.length; i += 1) {
    //           if (result[UserName].roles[i] === ROLE_ACESSO) {
    //               setAuth(true);                    
    //               return;           
    //           }
    //       }
    //     }  
    //     setAuth(false);    
    //     history.push("/unauthorized")
    //   }
    // );
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
      <Route path="/*">  
        <Redirect to="/home"/>  
      </Route> 
    </Switch>  
  )
};

export default Routes;
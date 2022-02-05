import React from "react";
import { useHistory } from 'react-router-dom';
import { Backdrop, CircularProgress } from "@material-ui/core";
import Cookies from "universal-cookie";

const Login = () => {  

    const history = useHistory();
    const cookies = new Cookies();    
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const User = urlParams.get('nomeUsuario') || cookies.get('UserName');    

    if (!User) {
        //
    } else {
        cookies.set('UserName', User);
        history.replace("/home");
    }
        
    return (
        <div style={{ textAlign: "center"}}>
            <Backdrop open>
                <CircularProgress style={{color: "#fff"}} color="inherit" />
            </Backdrop>
        </div>        
    )
}

export default Login;

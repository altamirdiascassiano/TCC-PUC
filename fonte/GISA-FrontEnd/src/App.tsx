import React, { Suspense } from "react";
import { BrowserRouter, Switch } from 'react-router-dom';
import { Backdrop, CircularProgress } from "@material-ui/core";
import { ThemeProvider, SnackbarProvider } from "framework-ui";
import Header from "./components/Header";
import Footer from "./components/Footer";

const Routes = React.lazy(() => import("./routes"))
// const Login = React.lazy(() => import("./pages/Login"))
// const Unauthorized = React.lazy(() => import("./pages/Unauthorized"))

const App = () => {

  const LoadingLazy = () => (
    <div>
      <Backdrop open>
        <CircularProgress style={{color: "#fff"}} color="inherit" />
      </Backdrop>
    </div>
  );

  return (
    <ThemeProvider>
      <Suspense fallback={<LoadingLazy />}>
        <SnackbarProvider>
          <BrowserRouter> 
            <Switch>
              {/* <Route exact path="/" component={Login} />   */}
              {/* <Route exact path="/unauthorized" component={Unauthorized} /> */}
              <div className="App">
                <Header />                      
                <Routes />          
                <Footer /> 
              </div>   
            </Switch> 
          </BrowserRouter>
        </SnackbarProvider>
      </Suspense>
    </ThemeProvider>
  );
};

export default App;

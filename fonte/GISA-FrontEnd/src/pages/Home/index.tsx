import React, { useState, useEffect } from "react";
import { Card, Button } from "framework-ui";
import moment from "moment";
import 'moment/locale/pt-br';

const Home = () => {
  const pageStyle={    
    display: "grid",
    gridTemplateColumns: "repeat(1, 1fr)",
    gridGap: 16, 
    maxWidth: 1130,
    margin: "0 auto" 
  }
  
  useEffect(() => {
    sessionStorage.clear();
  }, [])

  return (
    <div style={pageStyle}>         
      <Card>        
        {/* <Card.Footer>
          <Link style={{ textDecoration: "none" }} to="/prestadores">
            <Button color="primary" size="small" variant="text">
              <h3 style={{color: ColorsDefault.secondary}}>Prestadores</h3>
            </Button>
          </Link>
          <Link style={{ textDecoration: "none" }} to="/prestadores/prestador">
            <Button color="primary" size="small" variant="text">
              <h3 style={{color: ColorsDefault.primary}}>Novo Prestador de Serviço</h3>
            </Button>
          </Link>
        </Card.Footer> */}
      </Card>
      
    </div>
  )
}

export default Home

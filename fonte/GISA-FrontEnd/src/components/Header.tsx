import React from "react";
import LocalHospitalIcon from '@mui/icons-material/LocalHospital';
import Logo from '../assets/ico2.png'
import  AppBar from "./AppBar";
import Menu from "./Menu";
//import { Link } from "react-router-dom";


const Header = () => {

  return (
      <AppBar
        logo={Logo}
        renderSidebar={Menu}        
      />
  );
};

export default Header;

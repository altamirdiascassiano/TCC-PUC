/* eslint-disable spaced-comment */
/* eslint-disable react/prop-types */
/* eslint-disable spaced-comment */
/* eslint-disable @typescript-eslint/ban-types */
import React from "react";
import { MenuDrawer } from "framework-ui";
import { useHistory } from "react-router-dom";
import { Home as HomeIcon, Person, Assignment } from "@material-ui/icons";
import MedicationIcon from '@mui/icons-material/Medication';

type Props = {
  open: boolean;
  setOpen: Function;
};

const Menu: React.FC<Props> = ({ open, setOpen }) => {
  const history = useHistory();

  const handleNavigation = (item: any) => () => {
    setOpen(false)();
    history.push(item.path);
  }

  const items = [
    { title: "Home", path: "/home", icon: HomeIcon, action: "" },
    { title: "Prestador", path: "/prestadores", icon: MedicationIcon, action: "" },
    { title: "Associado", path: "/associados", icon: Person, action: "" },
    { title: "Processo", path: "/processos", icon: Assignment, action: "" }
  ]

  return (
    <MenuDrawer open={open} setOpen={setOpen}>
      {(Drawer: any) => items.map((item) => (
        <Drawer.Item
          icon={item.icon}
          key={item.title}
          title={item.title}
          onClick={handleNavigation(item)}
          selected={history.location.pathname === item.path}          
        />
      ))}
    </MenuDrawer>
  );
};

export default Menu;

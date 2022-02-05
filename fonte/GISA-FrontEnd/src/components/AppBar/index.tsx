/* eslint-disable react/prop-types */

import React, { useState } from "react";
import { Close as CloseIcon, Menu as MenuIcon } from "@material-ui/icons";
import {
  AppBar as MaterialAppBar,
  Toolbar,
  IconButton,
  Theme,
  makeStyles,
  createStyles
} from "@material-ui/core";

type Props = {
  renderSidebar: (value: any) => void;
  logo: string | React.ReactElement;
};

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    appBar: {
      zIndex: 1200,
      backgroundColor: theme.palette.error.dark
    },
    logoWrapper: {
      marginLeft: 8
    },
    imageWrapper: {
      width: 110,
      height: 30,
      backgroundColor: (props: { empty: boolean }) =>
        props.empty ? theme.palette.grey[100] : "transparent",
      borderRadius: (props: { empty: boolean }) => (props.empty ? 2 : 0)
    },
    image: {
      width: "100%",
      height: "100%",
      objectFit: "contain"
    },
    rightColumn: {
      display: "flex",
      marginLeft: "auto",
      alignItems: "center"
    }    
  })
);

const AppBar: React.FC<Props> = ({logo, renderSidebar}) => {
  const classes = useStyles({ empty: !logo });
  const [open, setOpen] = useState(false);

  const handleMenu = (state: boolean) => () => {
    setOpen(state);
  };

  return (
    <>
      <MaterialAppBar
        elevation={1}
        color="transparent"
        className={classes.appBar}
        position="fixed"
      >
        <Toolbar>
          <IconButton color="inherit" onClick={handleMenu(!open)}>
            {open ? (
              <CloseIcon style={{ color: 'white' }} />
            ) : (
              <MenuIcon style={{ color: 'white' }} />
            )}
          </IconButton>
          <div className={classes.logoWrapper}>
            {!logo || typeof logo === "string" ? (
              <div className={classes.imageWrapper}>
                {logo && <img className={classes.image} src={logo} alt="" />}
              </div>
            ) : (
              logo
            )}
          </div>
          <div className={classes.rightColumn}>
            {/* <IconButton style={{color: 'white'}} edge="end" size="small" className={classes.avatarButton}>
                Olá, {userImage}
            </IconButton> */}
          </div>
          
        </Toolbar>
      </MaterialAppBar>
      {renderSidebar && renderSidebar({ open, setOpen: handleMenu })}
    </>
  );
};

export default AppBar;

import React,{ useState} from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { DrawerComponents } from './DrawerComponents';
import { Avatar, Button, Grid, ListItem, ListItemAvatar, ListItemText, Menu } from '@material-ui/core';
import { MenuContext } from './MenuContext';
import { LeaveFromChat } from '../ChatComponent/LeaveFromChat';
import { AddMember } from '../ChatComponent/AddMember';
import MoreVertIcon from '@material-ui/icons/MoreVert';
const useStyles = makeStyles((theme) => ({
    AppBar:{
        background:"#5682a3",
        position:"relative",
        padding:"0",
        margin:"0",
        height:"11vh"
    },
    large: {
        width: theme.spacing(5),
        height: theme.spacing(5),
      }

  }));

export const MenuComponent = ({children}) => {
    const classes = useStyles();
    let [chat, setChat]=useState({});
    const [anchorEl, setAnchorEl] = React.useState(null);
    console.log(chat.countMembers);

    let setChatT=(chat)=>{
        console.log("new chat", "        ", chat)
        setChat(chat);
    }

    const handleClick = (event) => {
      setAnchorEl(event.currentTarget);
    };
  
    const handleClose = () => {
      setAnchorEl(null);
    };
    let MenusItems=[AddMember,LeaveFromChat];


    return(
        <React.Fragment>
            {chat.countMembers}
            <MenuContext.Provider value={{setChatT,chat}}>
            <AppBar color="inherit" className={classes.AppBar} >
                    <Grid container>
                        <Grid item xs={4}>
                            <Toolbar>
                                <DrawerComponents isAuth/>
                                <Typography variant="h6" style={{color:"white",display:"inline-block"}}>
                                    Messenger
                                </Typography>
                            </Toolbar>
                        </Grid>
                        {Object.keys(chat).length===0?
                        null
                        :
                        <Grid item xs={8} container>
                            <Grid item xs={11}>
                                <ListItem button  
                                    style={{height:"100%",color:"white", width:"100%",margin:"0", display:"inline-block" }} 
                                    disableRipple >
                                    <ListItemAvatar style={{display:"inline-block"}}>
                                        <Avatar className={classes.large}/>
                                    </ListItemAvatar>
                                    <ListItemText style={{display:"inline-block"}}
                                        primary={
                                            <div>
                                                {chat.name}
                                            </div>
                                        }
                                        secondary={
                                            <Typography component="span" varian="body2">
                                                {chat.countMembers} members, {chat.countMembersOnline} online
                                            </Typography>
                                        }
                                    />
                                </ListItem>
                            </Grid>
                            <Grid item xs={1}>
                                <Button
                                    onClick={handleClick}
                                    style={{height:"100%",color:"white", width:"100%" }} 
                                    disableRipple>
                                    <MoreVertIcon />
                                </Button>
                            </Grid>
                        </Grid>
                        }
                    </Grid>
            </AppBar>
                <Menu
                    id="simple-menu"
                    anchorEl={anchorEl}
                    keepMounted
                    onClick={handleClose}
                    open={Boolean(anchorEl)}
                    onClose={handleClose}
                >
                    {MenusItems.map((El,i)=><El key={i} guid={chat.guid}/>)}
                </Menu>
                {children}
            </MenuContext.Provider>
        </React.Fragment>
    )
};
import React, {useState} from "react";
import SwipeableDrawer from '@material-ui/core/SwipeableDrawer';
import {IconButton,List, ListItem, ListItemText,ListItemIcon} from "@material-ui/core";
import MenuIcon from '@material-ui/icons/Menu';
import Chat from '@material-ui/icons/Chat';
import { BackDrop } from "../tools/BackDrop";
import { CreateChatComponent } from "../ChatComponent/CreateChatComponent";
import { UserDrawer } from "../UserComponent/UserDrawer";

export const DrawerComponents=()=>{



	let ListYes=[
		{
			text: "New Chat",
			icon: <Chat/>,
			comp: <CreateChatComponent/>
		},
	]
	let [open, setOpen]=useState(false);
	var drawer=<UserDrawer />;
	const handler=()=>{
		setOpen(!open);
	}
    const list=   <List>
					{ListYes.map((el,i)=>
						<BackDrop key={i} OpenComponent={
							<ListItem button>
								<ListItemIcon>{el.icon==="none"? null:el.icon}</ListItemIcon>
								<ListItemText>
									{el.text}						
								</ListItemText>
                       		</ListItem>
						} ComponentToView={
							<div>
								{el.comp}
							</div>
						}/>
						
					)}
                  </List>
	return(
		<div style={{display:"inline-block"}}>
			<IconButton 
				edge="start" 
				color="inherit" 
				aria-label="menu"
				onClick={handler}>
                <MenuIcon />
            </IconButton>
			<SwipeableDrawer
			anchor={"left"}
			open={open}
			onClose={handler}
			onOpen={()=>{}}
			>
				{drawer}
				<div style={{width:"250px"}}>
					{list}	
				</div>
			</SwipeableDrawer>
		</div>
	)
}
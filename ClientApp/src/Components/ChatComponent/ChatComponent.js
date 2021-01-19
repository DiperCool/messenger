import React from 'react';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Typography from '@material-ui/core/Typography';


export const ChatComponent = React.memo(({guid, chats,name, lastMessage,setNewMessage, newUser}) => {


    let text=lastMessage===null?"Empty":lastMessage.content.slice(0,30);
    return(
        <ListItem button divider>
            <ListItemText
            primary={<b>{name}</b>}
            secondary={
                <React.Fragment>
                <Typography
                    component="span"
                    variant="body2"
                    color="textPrimary"
                >
                    {lastMessage===null?"":lastMessage.creator.login}: {text.length===30?(text+"..."):text}
                </Typography>
                </React.Fragment>
            }
        />
        </ListItem>
    )
});
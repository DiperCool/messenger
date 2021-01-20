import { Grid, List } from '@material-ui/core';
import React,{useCallback, useContext, useEffect,useState} from 'react';
import { sendReq } from '../../Api/Auth/sendReq';
import { MenuContext } from '../MenuComponent/MenuContext';
import { UserContext } from '../UserComponent/UserContext';
import { ChatComponent } from './ChatComponent';
import { ChatView } from './ChatView';



export const ChatsComponent = () => {
    let [chats, setChats]=useState([]);
    let [active,setActive]= useState(null);
    let {setChatMenu}= useContext(MenuContext)
    let {Auth}= useContext(UserContext);
    


    useEffect(()=>{
        let fetch=async ()=>{
            let res= await sendReq("get", "api/chat/getChats");
            res.data.push()
            setChats(res.data);
        }
        fetch();
    },[])
    useEffect(()=>{
        Auth.con.on("addChat", (newChat)=>{
            setChats([newChat, ...chats]);
        })
        Auth.con.on("leave", chat=>{
            deleteChat(chat.guid);
        })
        chats.forEach(el=>{
            var guid= el.guid;
            Auth.con.on(guid+"-newMessage", message=>{
                setNewMessage(guid,message);
            })
            Auth.con.on(guid+"-newMember", user=>{
                newUser(guid, user);
            });
            Auth.con.on(guid+"-leaveMember", user=>{
                leaveUser(guid, user);
            })
        })

        return ()=>{

            chats.forEach(el=>{
                var guid= el.guid;
                Auth.con.off(guid+"-newMessage");
                Auth.con.off(guid+"-newMember");
                Auth.con.off(guid+"-leaveMember");
            })

            Auth.con.off("addChat");
            Auth.con.off("leave");
        }
    })
    const getChatIdByGuid=useCallback(guid=>chats.findIndex((el)=>el.guid===guid), [chats]);
    useEffect(()=>{
        if(active===null){
            setChatMenu(null);
            return;
        }
        setChatMenu(Object.assign({},chats[getChatIdByGuid(active)]));
    },[chats, active, getChatIdByGuid, setChatMenu])

    const setNewMessage=useCallback((guid, message)=>{
        let chatId= getChatIdByGuid(guid)
        let items= [...chats];
        items[chatId].messages=[...items[chatId].messages, message]
        items[chatId].lastMessage=message;
        let swap= items[chatId]
        items.splice(chatId, 1);
        items.unshift(swap);
        setChats(items);
    },[chats,getChatIdByGuid])

    const onClickAChat=useCallback(guid=>{
        setActive(guid);
        setChatMenu(chats[getChatIdByGuid(guid)])
    
    },[chats,getChatIdByGuid, setChatMenu]);

    const setNewMessages=useCallback((guid, messages)=>{
        let chatId= getChatIdByGuid(guid)
        let items= [...chats];
        items[chatId].messages=[...messages,...items[chatId].messages]
        setChats(items);
    },[chats,getChatIdByGuid])

    const newUser= useCallback((guid, user)=>{
        let chatId= getChatIdByGuid(guid);
        let items= [...chats];
        items[chatId].countMembers+=1;
        if (user.isOnline) {
            items[chatId].countMembersOnline+=1;
        }
        setChats(items);
    },[chats,getChatIdByGuid]);
    const leaveUser= useCallback((guid, user)=>{
        let chatId= getChatIdByGuid(guid);
        let items= [...chats];
        items[chatId].countMembers-=1;
        if (user.isOnline) {
            items[chatId].countMembersOnline-=1;
        }
        setChats(items);
    },[chats,getChatIdByGuid]);
    const deleteChat= useCallback((guid)=>{
        if(guid===active){
            setActive(null);
        }
        let items= [...chats];
        setChats(items.filter(x=>x.guid!==guid));
    }, [chats, active]);

    const curChat= active===null?null:chats[getChatIdByGuid(active)];
    return(
        <div>
            <Grid container style={{height:"89vh"}}>
                <Grid item xs={4}>
                    <List style={{"overflow":"scroll", "overflowX":"hidden",height:"83vh"}}>
                        {chats.map((el,i)=>
                            <div key={el.guid }onClick={()=>{onClickAChat(el.guid)}}>
                                <ChatComponent
                                chats={chats}
                                guid={el.guid} name={el.name} 
                                lastMessage={el.lastMessage}
                                setNewMessage={setNewMessage}
                                newUser={newUser}
                                />
                            </div>
                        )}
                    </List>
                </Grid>
                <Grid item xs={8}>
                   {active===null?"": <ChatView 
                                            guid={curChat.guid} 
                                            messages={curChat.messages}
                                            setNewMessages={setNewMessages} 
                                            ></ChatView>}
                </Grid>
            </Grid>
        </div>
    )
};
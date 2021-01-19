import React, { useState, useEffect } from "react";
import {UserContext} from "./UserContext";
import {sendReq} from "../../Api/Auth/sendReq";
import ControlJwt from "../../Api/Auth/ControlJwt";
import { HubConnectionBuilder } from '@microsoft/signalr';
import { config } from "../../config";
export const User=({children})=>{

    let [Auth, setAuth]= useState({
        login:"",
        isAuth:false,
        isPending:true,
        con:null
    });


    const setGetLogin=async()=>{
        setAuth({isPending: true})
        var result=await sendReq("get", "api/user/getUser")
        if(result.status===200){

            const newConnection = new HubConnectionBuilder()
                .withUrl(config.url+'ChatHub', { accessTokenFactory: () => ControlJwt.getJwt()})
                .build()
            newConnection.start().then(()=>{
                setAuth({
                    login:result.data.login,
                    ava:result.data.currentAva===null?"":result.data.currentAva.webPath,
                    isAuth:true,
                    isPending:false,
                    con:newConnection
    
                })
            })
            return;
        }
        setAuth({
            login:"",
            isAuth:false,
            isPending:false
        });
    }

    useEffect(()=>{
        setGetLogin();
    },[]);
    useEffect(()=>{
        if(Auth.con==null){
            return;
        }
        Auth.con.on(Auth.login+"ChangeAvatar", (url)=>{
            setAuth({...Auth, ava:url});
        })

        return ()=>{
            Auth.con.off(Auth.login+"ChangeAvatar");
        }
    });


    const setLoginAndSetAuth=(login)=>{
        setAuth({
            login:login,
            isAuth:true,
            isPending:false
        });
    }


    if(Auth.isPending) return <div>загрузка</div>;

    return(
        <UserContext.Provider value={{Auth,setLoginAndSetAuth,setGetLogin}}>
            {children}
        </UserContext.Provider>
    )
}
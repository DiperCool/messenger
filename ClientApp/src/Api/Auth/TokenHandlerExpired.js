
import axios from "axios";
import {config} from "../../config";
import jwt from "./ControlJwt";


export const TokenHandlerExpired=async (response, callback, redirect)=>{
    try{
        if(response.headers["token-expired"]&&response.status===401){
            let res=await axios.post(config.url+"api/auth/refresh", {
                "Token": jwt.getJwt(),
                "RefreshToken": jwt.getRefreshToken()
            })
            jwt.setJwt(res.data.token);
            jwt.setRefreshToken(res.data.refreshToken);
            return await callback();
        }
        if(redirect){
            window.location.href="/login";
        }
        return response;    
    }
    catch{
        if(redirect){
            window.location.href="/login";
        }
        return response;
    }
}
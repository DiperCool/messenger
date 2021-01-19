import axios from "axios";
import {TokenHandlerExpired} from "./TokenHandlerExpired";
import {config} from "../../config";
import Jwt from "./ControlJwt";
export const sendReq=async(method,url,{data={},headers={},redirect=false}={})=>{
    try{
        const res= await axios({
            method:method,
            url:config.url+url,
            data:data,
            headers: {
                'Authorization': "Bearer "+Jwt.getJwt(),
                ...headers
            }
        })
        return res;
    }
    catch(e){
        return await TokenHandlerExpired(e.response,()=>sendReq(method, url, {headers:headers,redirect:redirect,data:data}),redirect);
    }
}
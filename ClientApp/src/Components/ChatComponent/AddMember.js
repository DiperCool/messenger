import { Button, MenuItem, TextField } from "@material-ui/core";
import React, { useRef } from "react";
import { sendReq } from "../../Api/Auth/sendReq";
import { BackDrop } from "../tools/BackDrop";
export const AddMember=({guid})=>{

    const refInput=useRef(null);
    const addMemberHandler=()=>{
        sendReq("post", "api/chat/addMembers", {data:{
            login:refInput.current.value,
            guid:guid
        }, headers:{
            'Content-Type': 'application/json'
        }});
    }

    return (
        <BackDrop 
            OpenComponent={<MenuItem>Add Member</MenuItem>} 
            ComponentToView={
                <div>
                    <TextField inputRef={refInput}/>
                    <Button onClick={addMemberHandler}>Add</Button>
                </div>
            }     
            />
    )
}

// [Required]
// public string guid{get;set;}
// [Required]
// public string Login{get;set;}
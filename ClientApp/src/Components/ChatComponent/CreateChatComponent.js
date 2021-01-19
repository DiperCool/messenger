import { Button, Grid, TextField } from '@material-ui/core';
import React,{useRef} from 'react';
import {sendReq} from "../../Api/Auth/sendReq";


export const CreateChatComponent = () => {
    

    let nameRef= useRef(null);
    
    let click=()=>{
        sendReq("post", "api/chat/createChat", {data:{"name":nameRef.current.value}})
    }
    return(
        <div>
            <Grid
                container
                direction="column"
                justify="center"
                alignItems="center"
                spacing={1}
            >
                <Grid
                    item>
                    <TextField id="standard-basic" placeholder="Enter a name" inputRef={nameRef}/>
                </Grid>
                <Grid
                    item>
                    <Button variant="contained" color="primary" onClick={click}>Create</Button>
                </Grid>
            </Grid>
        </div>
    )
};
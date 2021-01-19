import React, { useState } from 'react';
import { Backdrop, Divider } from '@material-ui/core';
import Close from '@material-ui/icons/Close';
import { IconButton } from '@material-ui/core';
export const BackDrop=({OpenComponent, ComponentToView})=>{
    


    const [open, setOpen]=useState(false);

    const toggleOpen=()=>{
        setOpen(!open);
    }
    return(
        <div>
            <div onClick={toggleOpen}>
            {OpenComponent}
            </div>
            <Backdrop open={open} style={{zIndex:2}} onClick={toggleOpen}>
                <div style={{backgroundColor:"white", zIndex:4, padding:"10px"}} onClick={(event)=>{ event.stopPropagation(); }}>
                    <div>
                        <IconButton onClick={toggleOpen}>
                            <Close/>
                        </IconButton>
                        <Divider light/>
                    </div>
                    <br/>
                    {ComponentToView}
                </div>
            </Backdrop>
        </div>
    )
}
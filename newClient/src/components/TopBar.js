import React, {useEffect, useState} from "react";
import {Button, Paper} from '@mui/material';
import Box from '@mui/material/Box';
import {useNavigate} from "react-router-dom";

export default function TopBar({unlogin, isAuthenticated}) {
    const navigate = useNavigate()

    return (
        <Box sx={{display: 'flex', flexDirection: 'row', justifyContent: 'center'}}>
            {!isAuthenticated
                ?
                <Button onClick={() => navigate("login")}>Login</Button>
                :
                <div>
                    <Button onClick={() => navigate("/reports")}>Generate Report</Button>
                    <Button onClick={() => navigate("/students")}>Manage students</Button>
                    <Button onClick={() => unlogin()}>Exit</Button>
                </div>}
        </Box>
    )
}
class Jwt{

    setJwt(token){
        localStorage.setItem("token", token);
    }

    getJwt(){
        return localStorage.getItem("token")
    }

    setRefreshToken(token){
        localStorage.setItem("refreshToken", token);
    }

    getRefreshToken(){
        return localStorage.getItem("refreshToken");
    }
}

export default new Jwt();
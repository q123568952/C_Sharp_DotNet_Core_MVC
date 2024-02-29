// firebase 
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.7.1/firebase-app.js";

import { GoogleAuthProvider, getAuth, signInWithPopup, FacebookAuthProvider } from "https://cdnjs.cloudflare.com/ajax/libs/firebase/10.7.1/firebase-auth.min.js";


var firebaseConfig = {
	apiKey: "AIzaSyAgYAiyLyPLv7ygqVmllqn7HyKFl_lf-Ms",
	authDomain: "dropcat-368e5.firebaseapp.com",
	projectId: "dropcat-368e5",
	storageBucket: "dropcat-368e5.appspot.com",
	messagingSenderId: "217114069588",
	appId: "1:217114069588:web:2627140e38f3087d007bc1"
};


// Initialize Firebase
var app = initializeApp(firebaseConfig);
var provider = new GoogleAuthProvider();
var auth = getAuth();
auth.useDeviceLanguage();

//facebook第三方登入
var providerf = new FacebookAuthProvider();
var facebookLogin = document.getElementById("facebook");

facebookLogin.addEventListener("click", function() {
	signInWithPopup(auth, providerf)
		.then((result) => {
			var credential = FacebookAuthProvider.credentialFromResult(result);
			var accessToken = credential.accessToken;
			var user = result.user;
			//console.log(user);

			user.getIdToken(true).then(function(idToken) {
				console.log(idToken);

				$.ajax({
					url: '/verifyToken',
					method: 'POST',
					contentType: 'application/json',
					data: JSON.stringify({ idToken: idToken }),
					success: function(token) {

						console.log("Response:", token);

						localStorage.setItem('JWT777', token);

						// 很重要的JWT放到Cookie
						// 新增1個cookie
						//document.cookie = `tokenFromJava=${token}`;
						console.log(" 新增了cookie，印出 document.cookie" + document.cookie);

						window.location.href = "/dropcat";
						console.log("接收從後端伺服器來的response成功");
					},
					error: function(error) {
						if (error.status === 404) {

							var userEmail = result.user.email;
							sessionStorage.setItem('userEmailForRegistration', userEmail);
							window.location.href = '/register';

						} else {
							console.log('login error' + error.responseText);
						}
					}
				});
			}).catch(function(error) {
				console.log("getToken error:" + error);
			});
		})
		.catch((error) => {
			var errorCode = error.code;
			var errorMessage = error.message;
			var email = error.customData.email;
			var credential = FacebookAuthProvider.credentialFromError(error);
		});
})




//Google第三方登入
var googleLogin = document.getElementById("google");
googleLogin.addEventListener("click", function() {
	console.log("Google login clicked");

	signInWithPopup(auth, provider)
		.then((result) => {
			var credential = GoogleAuthProvider.credentialFromResult(result);
			var token = credential.accessToken;
			var user = result.user;
			//console.log(user);

			user.getIdToken(true).then(function(idToken) {
				console.log(idToken);

				$.ajax({
					url: '/verifyToken',
					method: 'POST',
					contentType: 'application/json',
					data: JSON.stringify({ idToken: idToken }),
					success: function(token) {

						console.log("Response:", token);

						localStorage.setItem('JWT777', token);

						// 很重要的JWT放到Cookie
						// 新增1個cookie
						//document.cookie = `tokenFromJava=${token}`;
						console.log(" 新增了cookie，印出 document.cookie" + document.cookie);

						window.location.href = "/dropcat";
						console.log(token);
						console.log("接收從後端伺服器來的response成功");

					},
					error: function(error) {
						if (error.status === 404) {

							var userEmail = result.user.email;
							sessionStorage.setItem('userEmailForRegistration', userEmail);

							window.location.href = '/register';
						} else {
							console.log('login error' + error.responseText);
						}
					}
				});
			}).catch(function(error) {
				console.log("getToken error:" + error);
			});

		}).catch((error) => {
			var errorCode = error.code;
			var errorMessage = error.message;
			var email = error.customData.email;
			var credential = GoogleAuthProvider.credentialFromError(error);
		});
})


// line登入
var channel_id = "2002695399";
var channel_secret = "61bbe92b14ff4a060c00344e9025e4b8";
//如果開發的話要改回http://localhost:8080/login
var uri = "https://dropcat.fun/login";
//var uri = "http://localhost:8080/login";

document.getElementById('line').addEventListener('click', function() {
	var client_id = channel_id;
	var redirect_uri = uri;
	var link = 'https://access.line.me/oauth2/v2.1/authorize?';
	link += 'response_type=code';
	link += '&client_id=' + client_id;
	link += '&redirect_uri=' + redirect_uri;
	link += '&state=login';
	link += '&scope=openid%20profile%20email';
	window.location.href = link;
})

// 傳授權碼到line取得token
var url = new URL(window.location.href);
var code = url.searchParams.get('code');
var id_token = "";

if (code != null) {
	$.ajax({
		method: "POST",
		dataType: 'json',
		url: "https://api.line.me/oauth2/v2.1/token",
		async: false,
		data: {
			grant_type: "authorization_code",
			code: code,
			redirect_uri: uri,
			client_id: channel_id,
			client_secret: channel_secret
		},
		success: function(data) {
			id_token = data.id_token;
			console.log(id_token);
		},
		error: function(error) {
			alert(error.responseText);
		}
	});
	$.ajax({
		method: "POST",
		dataType: 'json',
		async: false,
		url: "https://api.line.me/oauth2/v2.1/verify",
		data: {
			client_id: channel_id,
			id_token: id_token
		},
		success: function(data) {
			JSON.stringify(data);
			console.log(data);

			var userData = data;

			$.ajax({
				method: "POST",
				url: "/linelogin",
				contentType: "application/json",
				data: JSON.stringify(userData),
				success: function(response) {

					console.log("JWT:", response);
					localStorage.setItem('jwtline', response);
					//document.cookie = `tokenFromJava=${response}`;
					window.location.href = "/dropcat";
				},
				error: function(error) {
					if (error.status === 404) {

						var lineEmail = userData.email;
						console.log(lineEmail);
						sessionStorage.setItem('userEmailForRegistration', lineEmail);
						window.location.href = '/register';
					} else {
						console.log('login error' + error.responseText);
					}
				}
			});
		},
		error: function(error) {
			alert(error.responseText);
		}
	});
}


// 解析 JWT 函數
function parseJWT(token) {
	var base64Url = token.split('.')[1];
	var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
	var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
		return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
	}).join(''));

	return JSON.parse(jsonPayload);
}


// 一般登入
var login = document.getElementById("loginForm");

login.addEventListener("submit", function(event) {
	event.preventDefault();

	var username = document.getElementById("account").value;
	var password = document.getElementById("passwd").value;
	var rememberMe = document.getElementById("rememberMe");

	var loginData = {
		username: username,
		password: password
	};

	$.ajax({
		url: "/Login/Login",
		method: "POST",
		contentType: "application/json",
		data: JSON.stringify(loginData),
		success: function(response) {

			window.location.href = "/Main/ListUser";

		},
		error: function(xhr) {

			if (xhr.status === 400) {
				alert("帳號或密碼錯誤，請重新輸入");
			}
		}
	})
})

// 從Cookie中獲取用戶名
function getCookie(name) {
	var value = "; " + document.cookie;
	var parts = value.split("; " + name + "=");
	if (parts.length === 2) return decodeURIComponent(parts.pop().split(";").shift());
}

// 當頁面加載時檢查是否有記住的帳號
document.addEventListener("DOMContentLoaded", function() {
	var rememberedUsername = getCookie('rememberMe');
	if (rememberedUsername) {
		document.getElementById("account").value = rememberedUsername;
	}
});



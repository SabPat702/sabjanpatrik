fetch('https://jsonplaceholder.typicode.com/posts').then(respone => respone.json()).then(res => document.getElementById("ide").innerHTML = res[0].title)

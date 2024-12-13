// natív->hogyan?
const btn = document.createElement("button");

btn.onclick = function(){
    alert("Ez egy natív kód");
}

btn.innerHTML = "Natív gomb";

document.getElementById("nativ-button-container").appendChild(btn);

//react->mit akarok látni?
const gomb = React.createElement("button",
{
    onclick: function(){
        alert("Ez egy react kód");
    },
},
"React Gomb"
)
//első paraméter az elemet amit használni szeretnénk,
//második paraméter egy referencia egy natív DOM elemre, hivatkozásipont megadása
ReactDOM.render(gomb,document.getElementById("react-button-container"));
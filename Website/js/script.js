document.addEventListener("DOMContentLoaded", () => {
    visibilidadeBotao();
});

/* For the button "back to the top"*/
const backToTopButton = () => {
  const backToTopButton = document.querySelector('.back-to-top');
  if (backToTopButton) {
    window.addEventListener('scroll', () => {
      if (window.scrollY > 100) {
        backToTopButton.classList.add('show');
      } else {
        backToTopButton.classList.remove('show');
      }
    });
  }
};

backToTopButton();

/* This is to make the navbar smaller when someone scrolls down*/
window.addEventListener("scroll", function () {
  const navbar = document.getElementById("mainNavbar");
  if (window.scrollY > 50) {
    navbar.classList.add("shrink");
  } else {
    navbar.classList.remove("shrink");
  }
});

document.addEventListener("click", function () {
  const form = document.querySelector("form");

  form.addEventListener("submit", function (e) {
    e.preventDefault();
    form.innerHTML = "";

    const div = document.createElement("div");
    div.classList.add("text-center", "p-4");

    const titulo = document.createElement("h2");
    titulo.classList.add("text-success");
    titulo.textContent = "Obrigado pelo seu contacto!";

    const paragrafo = document.createElement("p");
    paragrafo.textContent = "Entraremos em contacto consigo brevemente 😊";

    div.appendChild(titulo);
    div.appendChild(paragrafo);
    form.appendChild(div);
  });
});

const lightbox = GLightbox({
  selector: '.glightbox',
  zoomable: true,
  touchNavigation: true,
});


function visibilidadeBotao(){
  let botao = document.getElementById("buttonAddCao")

  if (localStorage.getItem("token") != null){
    botao.style.display = "inline";

  } else{
    botao.style.display = "none";
  }
}

document.addEventListener("DOMContentLoaded", () => {
  visibilidadeBotao();
});

/* For the button "back to the top"*/
const backToTopButton = () => {
  const backToTopButton = document.querySelector(".back-to-top");
  if (backToTopButton) {
    window.addEventListener("scroll", () => {
      if (window.scrollY > 100) {
        backToTopButton.classList.add("show");
      } else {
        backToTopButton.classList.remove("show");
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

const lightbox = GLightbox({
        selector: ".glightbox",
        zoomable: true,
        touchNavigation: true,
    });

function visibilidadeBotao() {
  let botao = document.getElementById("buttonAddCao");

  if (localStorage.getItem("token") != null) {
    botao.style.display = "block";
  } else {
    botao.style.display = "none";
  }
}

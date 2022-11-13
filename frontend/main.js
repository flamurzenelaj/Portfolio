const selectElement = (s) => document.querySelector(s);
const navLinks = document.querySelectorAll(".nav-link");

selectElement(".burger-menu-icon").addEventListener("click", () => {
    selectElement(".nav-list").classList.toggle("active");
    selectElement(".burger-menu-icon").classList.toggle("toggle")

    navLinks.forEach((link, index) => {
        if (link.style.animation){
            link.style.animation = ""
        }else{
            link.style.animation = `navLinkAnimate 0.5s ease forwards ${ index/7 + 0.5}s`
            console.log(index/7 + 0.5)
        }
    })
});

navLinks.forEach(link => {
    link.addEventListener("click", () => {
        selectElement(".nav-list").classList.toggle("active");
        selectElement(".burger-menu-icon").classList.toggle("toggle");

        navLinks.forEach((link, index) => {
            if (link.style.animation){
                link.style.animation = ""
            }else{
                link.style.animation = `navLinkAnimate 0.5s ease forwards ${ index/7 + 0.5}s`
                console.log(index/7 + 0.5)
            }
        })
    })
})


const progress = document.querySelector('.progress-bars-wrapper');
const progressBarPrecents = [97,89,85,67,87,80,53];

window.addEventListener("scroll", ()=>{
    if(window.pageYOffset + window.innerHeight >= progress.offsetTop){
        document.querySelectorAll('.progress-precent').forEach((el,i)=>{
            el.style.width = `${progressBarPrecents[i]}%`
        })
    }
})

function reveal() {
    var reveals = document.querySelectorAll(".reveal");
  
    for (var i = 0; i < reveals.length; i++) {
      var windowHeight = window.innerHeight;
      var elementTop = reveals[i].getBoundingClientRect().top;
      var elementVisible = 50;
  
      if (elementTop < windowHeight - elementVisible) {
        reveals[i].classList.add("active");
      } else {
        reveals[i].classList.remove("active");
      }
    }
  }
  
  window.addEventListener("scroll", reveal);

// Get data from API

fetch('https://localhost:7016/api/Skill')
.then(res => {
    return res.json();
})
.then(data =>{
    data.forEach(skill => {
        const markup = `
        <div class="progress-bar reveal">
        <p class="progress-text">
            ${skill.skillName}
          <span>${skill.skillPrecent}</span>%
        </p>
        <div class="progress-precent"></div>
      </div>
      `

        document.querySelector('.progress-bars-wrapper').insertAdjacentHTML('beforeend', markup)
    })
})
.catch(error=>console.log(error));


// Post data to API

const form = document.getElementById('form');

form.addEventListener('submit', function(e){
    e.preventDefault();

    var name = document.getElementById('username').value;
    var email = document.getElementById('email').value;
    var message = document.getElementById('message').value;

    fetch("https://localhost:7016/api/ContactForm", {
        method: 'POST',
        body: JSON.stringify({
            name:name,
            email:email,
            message:message
        }),
        headers:{
            "Content-Type":"application/json;"
        }
    })
    .then(function(response){
        return response.json()
    })
    .then(function(data){
        console.log(data)
    })
})
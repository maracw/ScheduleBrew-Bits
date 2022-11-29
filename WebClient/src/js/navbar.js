export default function getNavBar(activePage){

let customBar = "";
customBar=`<div class="container-fluid">
<ul class="navbar-nav">
  <li class="navbar-brand">Event Application</li>
  <li class="nav-item">
    <a class="nav-link ${activeHome}" href="${linkHome}" id="home">Home</a>
  </li>
  <li class="nav-item">
    <a class="nav-link ${activeStatus}" href="${linkStatus}" id="Status">Status</a>
  </li>
  <li class="nav-item">
    <a class="nav-link ${activeAbout}" href="${linkAbout}" id="About">About</a>
  </li>
</ul>
</div>`

document.getElementById("navbar").innerHTML = customBar;

}

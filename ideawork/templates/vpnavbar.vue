<template>
    <div class="nav-side-menu">
        <div class="brand">
            <a class="navbar-brand" href="#">
                <router-link to="/"><img :src="'catalogos/' + app.empresabase + '/img/logo.png'" @click="SetBodyInicial()" width="120" height="36" alt=""></router-link>
            </a>
            <label class="wellcome">Bem vindo: {{desusuario}}<a class="logout" href="./"><i class="fa fa-sign-out fa-lg"></i></a></label>
        </div>

        <i class="fa fa-bars fa-2x toggle-btn" data-toggle="collapse" data-target="#menu-content"></i>

        <div class="menu-list">

            <ul id="menu-content" class="menu-content collapse out">
                <li @click="alterClass(1)" id="1" class="active">
                    <router-link to="/">
                        <i class="fa fa-dashboard fa-lg"></i> DashBoard
                    </router-link>
                </li>

                <li @click="alterClass(2)" id="2">
                    <router-link to="/listajobs"><i class="fa fa-folder-open fa-lg"></i> Job</router-link>
                </li>

                <li @click="alterClass(3)" id="3">
                    <router-link to="/listachecklist"><i class="fa fa-check-square fa-lg"></i> Check-list</router-link>
                </li>

                <li data-toggle="collapse" data-target="#cadastros" class="collapsed" @click="alterClass(4)" id="4">
                    <a href="#"><i class="fa fa-gears fa-lg"></i> Cadastros <span class="arrow"></span></a>
                </li>
                <ul class="sub-menu collapse" id="cadastros">

                    <li @click="alterClass(5)" id="5">
                        <router-link to="/listausuarios">Usuários</router-link>
                    </li>
                    <li @click="alterClass(6)" id="6">
                        <router-link :to="{name:'CatalogoCaracteristicas', params: { tipo: 'T' }}">Tabelas</router-link>
                    </li>
                    <li @click="alterClass(7)" id="7">
                        <router-link to="/listaclientes">Clientes</router-link>
                    </li>
                </ul>

            </ul>

        </div>
    </div>

</template>
<script src="../js/vuecomponents/vpnavbar.js"></script>

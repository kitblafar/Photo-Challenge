<script>

import { sha256 } from 'js-sha256';

export default {
    name: 'HostLogin',
    props: ['strings'],
    data() {
        return {
            input: {
                password: ""
            }
        }
    },
    methods: {
        async login() {
            //make sure password not empty
            if (this.input.password !== "") {
                this.$axios({
                    method: "post",
                    url: this.$serverAddress+"authenticate/host",
                    data:{password: sha256(this.input.password)},
                    withCredentials: false,
                })
                    .then((res) => {
                        if (res.data === true) {
                            console.log("authenticated");
                            this.$router.push('/main/host');
                        }
                        else {
                            document.getElementById('failLoginNotify').style.display = "block";
                        }
                    })
                    .catch(() => {
                        document.getElementById('failLoginNotify').style.display = "block";
                    });

            } else {
                document.getElementById('failLoginNotify').style.display = "block";
                this.$router.push('/main/login')
            }

        },
        close() {
            document.getElementById('failLoginNotify').style.display = "none";
        }
    }
}

</script>

<template>
  <div class="section">
    <h1 class="title">Host Login</h1>
    <div class="field">
        <label class="label">Password:</label>
        <div class="control">
            <input class="input is-danger" placeholder="Password" v-model="input.password" type="password" id="password"
                name="password">
        </div>
    </div>

    <div class="control">
        <button class="button is-danger" value="save" @click="login">Submit</button>
    </div>

    <div class="block" id="failLoginNotify" style="display:none">
        <div class="block ">
        </div>
        <div class="block">
            <div class="notification is-danger">
                <button @click="close" class="delete"></button>
                Login Failed. Wrong host username or password.
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped></style>
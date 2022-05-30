<template>
  <div class="hello">
    <!-- <h1>{{ msg }}</h1> -->
    <h1>MejvitCalendar</h1>
    <p>MejvitCalendar je aplikace, která umožňuje vytvářet přehled akcí jež se zpravidla konají fyzicky, ale je možné shlédnout jejich on-line přenos. Typicky se jedná o různé sportovní soutěže, zasedání zastupitelstev obcí, koncerty a další.</p>
    <p>Obvyklou vlastností takových akcí bývá jejich pravidelné opakování. V aplikaci jsou proto umožněna různá schémata pravidelného opakování, která se v běžném životě vyskytují (každý druhý týden, každou 3. středu v měsíci, atd.) Aplikace shromažďuje různé organizace, které pořádají tyto události. Událost pak má odkaz, kde je možné shlédnout přenos, dále volitelný časový plán podle kterého se opakuje a kategorii, do které událost spadá. </p>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'HelloWorld',
  props: {
    msg: String
  }
})
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>

<template>
	<div class="container">
		<div class="order">
			<h5>Orders</h5>
			<div>
				<p>
					Please enter your order in the following format:<br />
					<b>day time, dishes (separated by comma)</b><br />
					Ex.: 'morning, 1, 2, 3'
				</p>
				<p>
					<label for="input" style="width:60px;">Input: </label>
					<input type="text" v-model="input" id="input" style="width:300px;" />
				</p>
				<p style="text-align:center">
					<input type="button" @click="sendOrder" value="Send Order" />
				</p>
				<p>
					<label for="output" style="width:60px;">Output: </label>
					<input type="text" v-model="output" readonly style="width:300px;" />
				</p>
				<p><font color="red">{{ msg }}</font></p>
			</div>
			<br />
			<h5>History</h5>
			<div id="table">
				<table class="table">
					<thead>
						<tr>
							<th scope="col">Input</th>
							<th scope="col">Output</th>
						</tr>
					</thead>
					<tbody>
						<tr v-for="item in rowData" :key="item">
							<td>{{ item.input }}</td>
							<td>{{ item.output }}</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>
		<div class="order">
			<h5>Dishes for each day time:</h5>
			<p>
				<table style="margin:auto;width:400px">
					<thead>
						<tr>
							<th>#</th>
							<th>Dish Type</th>
							<th>Morning</th>
							<th>Night</th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<td>1</td>
							<td>Entr&eacute;e</td>
							<td>Eggs</td>
							<td>Steak</td>
						</tr>
						<tr>
							<td>2</td>
							<td>Side</td>
							<td>Toast</td>
							<td>Potato</td>
						</tr>
						<tr>
							<td>3</td>
							<td>Drink</td>
							<td>Coffee</td>
							<td>Wine</td>
						</tr>
						<tr>
							<td>4</td>
							<td>Dessert</td>
							<td><i>Not Applicable</i></td>
							<td>Cake</td>
						</tr>
					</tbody>
				</table>
			</p>
		</div>
	</div>
</template>

<script>
	import axios from 'axios'
	export default {
		name: 'Home',
		data() {
			return {
				msg: '',
				input: 'night,1,2,3',
				output: '',
				list: []
			}
		},
		computed: {
			rowData() {
				return this.list;
			}
		},
		methods: {
			addItem() {
				var my_object = {
					input: this.input,
					output: this.output
				};
				this.rowData.push(my_object)
			},
			sendOrder() {
				var array = this.input.split(",");
				var items_array = array.slice(1);

				//console.log(array);
				//console.log(items_array);

				var request_obj = {
					DayTime: array[0],
					Items: items_array
				}
				//console.log(request_obj);

				var json = JSON.stringify(request_obj);
				//console.log(json);

				const options = { headers: { 'Content-Type': 'application/json' } };

				// call axios
				axios.post('http://localhost:5000/Orders', json, options)
					.then(response => {
						this.msg = '';
						console.log('response->', response);
						this.output = response.data;
						this.addItem();
					})
					.catch(error => {
						console.log('error->', error);
						this.msg = error.response.data;
					});

			}
		}
	}
</script>

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

	thead td {
		padding: 10px 5px 10px 5px
	}

	td {
		padding: 2px 5px 2px 5px;
	}

	.container div {
		width: 50%;
		float: left;
		clear: none;
	}

	.order div {
		float: none;
		clear: none;
		width: 100%;
	}
</style>

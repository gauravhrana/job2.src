var HelloWorld = React.createClass({
	render: function() {
		return (
        
        <div>            
		  <p>
			Hello, <input type="text" placeholder="Your name here" />!
            <br/>
			It is {this.props.date.toTimeString()}
		  </p>
        </div>
	  );
	}
});

setInterval(function() {
	React.render(<HelloWorld date={new Date()} />, document.getElementById('example2')
  );
}, 5000);
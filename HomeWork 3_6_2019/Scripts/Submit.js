$(() => {
	$("#Cmnt").on('keyup', function () {
		const hasvalue = $("#Cmnt").val();
		$("#Csubbtn").prop("disabled", !hasvalue)
	});
});
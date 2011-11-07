var EditFacet = function () {
    function showHideFacetOptions() {
        var facetOptions = $("#FacetOptions");
        var addOption = $("#AddOption");
        var typeSelect = $("#Type");
        var val = typeSelect.val();

        if (val == "List") {
            facetOptions.show();
            addOption.show();
        }
        else {
            facetOptions.hide();
            addOption.hide();
        }
    };

    function htmlEncode(val) {
        return $('<div/>').text(val).html();
    };

    return {
        Initialise: function () {
            $('#AddValue_Button').click(function () {
                var index = $('.facetCount').length;
                var value = htmlEncode($('#AddValue_Value').val());
                $('#AddValue_Value').val('');
                if (value.trim() === '')
                    return;
                $('#FacetOptions').append(
                '<label for="Values[' + index + '].ValueBox">Value</label>' +
                '<input id="Values[' + index + '].ValueBox" name="Values[' + index + '].Value" type="text" disabled value="' + value + '" /><br />' +
                '<input type="hidden" name="Values[' + index + '].Id" value="0" />' +
                '<input type="hidden" name="Values[' + index + '].FacetId" class="facetCount" value="0" />');

            });

            showHideFacetOptions();
            
            $("#Type").change(showHideFacetOptions);
        }
    }
} ();




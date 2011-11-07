var MemberList = function () {

    function ConfirmDelete(ref) {
        return confirm("Are you sure you wish to delete member " + ref);
    };

    return {
        Initialise: function () {
            var deleteLinks = $('a[title*="ref_"]');
            for (var i = 0; i < deleteLinks.length; i++) {
                deleteLinks[i].click(function () {
                    return ConfirmDelete(deleteLinks[i].attr('title').replace('ref_','')); 
                });
            }
        }
    };
} ();
/// <reference path="K:\PROJECT REPOSITORY\NJFairground\NJFairground\NJFairground.Web\Scripts/jquery-2.1.1.min.js" />
/// <reference path="K:\PROJECT REPOSITORY\NJFairground\NJFairground\NJFairground.Web\Scripts/jquery-2.1.0-vsdoc.js" />
/// <reference path="K:\PROJECT REPOSITORY\NJFairground\NJFairground\NJFairground.Web\Scripts/common-script.js" />

(function ($, win) {
    $(document).ready(function () {
        try {
            var schemaData = $('#SchemaData').val();
            if (schemaData != '') {
                var schemaJsonData = JSON.parse(Base64Decode(schemaData));
                var $gridElement = $('#grid');

                if (schemaJsonData) {
                    $gridElement.SetupGrid({
                        modelSchema: schemaJsonData,
                        datatype: 'json',
                        pagerid: '#pager',
                        renderURL: '{0}/Admin/CustomerManager/GetCustomer'.format(virtualDirectory),
                        //editURL: '{0}/Admin/CustomerManager/SetCustomer'.format(virtualDirectory),
                        //editable: true,
                        //insertMode: 'none',
                        //autowidth: false,
                        //shrinkToFit: false,
                        searchOperators: true,
                    });
                }
            }
        } catch (ex) {
            console.log(ex);
        }
    });
}(jQuery, window));
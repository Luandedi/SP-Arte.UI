
$(document).ready(function () {
 
    $.ajax(
        {
            url: url_obras, type: "GET", dataType: 'json', cache: false, async: true
        })
        .done(function (json) {           
            var gallery_data = '';  
            $.each(json.data, function (i, value) {              
                gallery_data += ' <div class="gallery">';
                gallery_data += '<img src="' + value.url + '" width="200" height="250">';
                gallery_data += '<div class="checkbox">';
                gallery_data += '<label><input type="checkbox" id="cb'+i+' " value="'+value.id+'">' + value.nome + '</label>';
                gallery_data += '</div>';
                gallery_data += '</div>';

            });       
            $('#template').append(gallery_data);
            $('#container').append($('#template').html());
          
        })
        .fail(function () {
            console.log("Falha na requisiçao");
        });  
 
});
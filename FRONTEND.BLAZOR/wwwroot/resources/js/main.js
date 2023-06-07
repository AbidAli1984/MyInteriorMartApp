(function ($) {
    $('.navbar-brand-btn').click(function() {
        $(this).toggleClass('active');
        $('.mim-HomeSideMenu').toggleClass('show');     
    });
    
    $('.navbar-brand-offer-menu .navbar-brand-offer-btn').click(function() {
        $(this).toggleClass('active');
        $(this).closest('.category-list').next('.brand-category-list').find('.navbar-brand-offer-list').toggleClass('show'); 
    });

    $('.navbar-brand-offer-list .nav-link').click(function() {
        $('.navbar-brand-offer-menu .navbar-brand-offer-btn').click();
    });

    $('.mim-box-list a').click(function() {
        $('.navbar-brand-btn').click();
    });
})(window.jQuery); 

$(document).ready(function () {    
    $('#HideNumber').hide();
    $('#contactDetails').hide();

    $("#ViewNumber").click(function () {
        $("#ViewNumber").hide();
        $("#HideNumber").show();
        $(this).parent('.social-details').next('#contactDetails ').show();
    });

    $("#HideNumber").click(function () {
        $("#ViewNumber").show();
        $("#HideNumber").hide();
        $(this).parent('.social-details').next('#contactDetails ').hide();
    });  
    $(".gander").select2({
        placeholder: "Gander",
        allowClear: true,
    });
    $(".turnover").select2({
        placeholder: "Turnover",
        allowClear: true,
    });
    $(".natureofbusiness").select2({
        placeholder: "Nature of Business",
        allowClear: true,
    });
    $(".designation").select2({
        placeholder: "Designation",
        allowClear: true,
    });
    $(".country").select2({
        placeholder: "Choose one",
        allowClear: true,
    });
    $(".state").select2({
        placeholder: "Choose one",
        allowClear: true,
    });
    $(".city").select2({
        placeholder: "Choose one",
        allowClear: true,
    });
    $(".pincode").select2({
        placeholder: "Choose one",
        allowClear: true,
    });  

    $(".locality").select2({
        placeholder: "Choose one",
        allowClear: true,
    });

    $(".assembly").select2({
        placeholder: "Designation",
        allowClear: true,
    });
    $(".fcategory").select2({
        placeholder: "First Category",
        allowClear: true,
    });
    $(".scategory").select2({
        placeholder: "Second Category",
        allowClear: true,
    });

    $("#v-pills-tabContent form").on('submit',function(e) {
        console.log('test');
        e.preventDefault();
        var li_count = $('#v-pills-tab-listing a').length;
        var current_active = $('.# a.active').index();
  
        if(current_active<li_count){
          $('#v-pills-tab-listing a.active').next('a.nav-link').attr('data-toggle','pill').tab('show');
        }else{
          alert('Last Step');
        }
    });

    var selectAllItems = "#select-all";
    var checkboxItem = ":checkbox";

    $(selectAllItems).click(function() {
      
      if (this.checked) {
        $(checkboxItem).each(function() {
          this.checked = true;
        });
      } else {
        $(checkboxItem).each(function() {
          this.checked = false;
        });
      }
      
    });   
});


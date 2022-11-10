$(document).ready(function() {
  // Select the first tab by default

  $("#vertical_tab_nav > ul > li > a")
    .eq(0)
    .addClass("active");
  $("#vertical_tab_nav > div > article")
    .eq(0)
    .css("display", "block");

  // This assigns an onclick event to each tab link("a" tag) and passes a parameter to the showHideTab() function

  $("#vertical_tab_nav > ul").click(function(e) {
    if ($(e.target).is("a")) {
      /*Handle Tab Nav*/
      $("#vertical_tab_nav > ul > li > a").removeClass("active");
      $(e.target).addClass("active");

      /*Handles Tab Content*/
      var clickeindex = $("a", this).index(e.target);
      $("#vertical_tab_nav > div > article").css("display", "none");
      $("#vertical_tab_nav > div > article")
        .eq(clickeindex)
        .fadeIn();
    }

    $(this).blur();
    return false;
  });
}); //end ready

/* Accordian */
$(".tab_heading").click(function() {
  $("article").hide();
  var activeTab = $(this).attr("rel");
  $("#" + activeTab).slideDown();

  $(".tab_heading").removeClass("active");
  $(this).addClass("active");

  $("ul.tabs li a").removeClass("active");
  $("ul.tabs li a[rel^='" + activeTab + "']").addClass("active");
});
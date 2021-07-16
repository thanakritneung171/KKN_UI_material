/*!
TableDnD plug-in for JQuery
Version 0.8.1
* 
Copyright (C) 2014-2015 Tom Phane
Licensed under the GNU Affero GPL v.3 or, at the distributor's discretion, a later version.
See http://www.gnu.org/licenses#AGPL.
*/
/**
 Derived from code by Denis Howlett <denish@isocra.com>

 Allows dragging and dropping table rows.

 Configuration options:

 buttonState
   String comprising acceptable drag-button-index, or combination thereof e.g. any of:
   0,1,2,01,02,12,012
   plus zero or more of 'shift', ctrl', alt', 'meta' (all lower-case). Default '' (i.e. anything)
   Note: browser differences may be a problem (http://unixpapa.com/js/mouse.html)
 containerID
   Id of a scrollable div containing the draggable table. This div is scrolled when the drag
   pointer is near the div's top or bottom. If not provided, the table's parent div, if that is
   scrollable, will be used. Otherwise, the whole window is treated as the scrollable container.
 dragHandle
   Name of a class assigned to draggable cells, one or more of them in each row that is draggable.
   If you specify this class, then only the rows containing those classed cells will be draggable.
 dragClass
   Name of class (or space-separated classes) added to the dragged row for the duration of
   the drag, and then removed. Classing is more flexible than using dragStyles since it
   can be inherited by the row cells and other content. The default class is tDnD_whileDrag.
   So to use the default, simply customise this CSS class in your stylesheet.
 dragStyles
   A map of CSS style(s) applied to the dragged row during the drag. There are limitations
   to the styles for a row (e.g. you can't assign a border -- well you can, but it won't be
   displayed). (So instead consider using dragClass.) The map is as used in the jQuery
   .css(...) function. Default = purple italic text
 dropStyles
   A map of CSS style(s) applied to the dragged row when it is dropped. Again, there are
   limitations. The style(s) remain in effect. (So again, consider using dragClass
   which is simply added and then removed on drop.)
 onDrop
   Name of a function to be called when the row is dropped. The function must take 2 parameters:
   the table, and the array of row(s) that were dropped. You can work out the new order of the
   rows by using table.rows.
 onDragStart
   Name of a function to be called when a drag starts. The function must take 2 parameters:
   the table, and the array of row(s) which the user has started to drag.
 onRowsChanged
   Name of a function to be called after rows are reordered during a drag though the drag
   has not yet ended. The function takes 3 parameters: the table, the array of row(s) that
   were dropped, and the row upon which the drop occurred.
 onAllowDrop
   Name of a function to be called when drag-cursor is over a non-dragged row. The function
   takes 2 parameters: the array of dragged row(s), and the row under the cursor. The function
   must return true if the dragged row(s) may be dropped there, or else false.
 filterRegexp
   Regular expression used to decide which rows to include when mapping a table. Default: null

 Other ways to control behaviour:

 Apply class "nodrag" to each row which may not be dragged.
   By itself, this doesn't achieve much, as dragging other rows can change the order anyhow.
   Perhaps useful for first and last rows.
 Apply class "nodrop" to each row onto which dropping is not permitted, and/or which may not
   be re-positioned as a result of dropping elsewhere.

 Inside the onDrop method you can call function map(table) which returns an object that can
 be sent to the server. No descending is done when interrogating the table, so this only
 suits relatively simple tables. The returned object is js-map-like:
  {rid1:{cid1:val,cid2:val...},rid2:{cid1:val,cid2:val...}...}
 The keys are the id of the corresponding tr/td, or if no id, then its name, or if none,
 a string like 'rowN' or 'colN' where the N is a 1-based index
 The values are: '<empty>', or a single value, or a map of values {0:val1,1:val2....}

 Public methods:

 $.tableDnD.mapTable(table)
   Maps table in the manner described above

 Known problems:

 Version history:
 0.2-0.5: 2008
      Releases by Denis Howlett
 0.6: 2011-10-16
      Fork
      Added checks for suitable element-types
      Added real support for multiple <tbody> elements in a table
      Added support for multi-selection (only for jQuery >= 1.4 and browsers that provide dblclick events)
      Reduced no. of events processed
      Added support for specific buttons and states
      Tolerate pointer 'jiggle' e.g. from in-cell click without triggering a drag
      Un-dragged rows classed "nodrop" treated as "fixed position", regardless of where drop occurs
      During drags, apply dragStyles (if it exists) before, not instead of, dragClass if that exists
      Added support for onRowsChanged callback
      Added missing default onAllowDrop
      Removed redundant scrollAmount parameter
      Removed unused default serializeParamName
      Removed redundant updateTables()
      (Hopefully, not much tested) improved browser compatibility
 0.6.1: 2012-08-10
      Also work with later jquery
      Fix input-editing in clicked row
 0.7: 2014-05-28
      Revise plugin structure
	  Rationalise 'on*' style- and class-parameter names INCOMPATIBLE
      Apply dragStyles after, not before, dragClass
      Also work with later jquery
      Replaced table-serialisers with map* funcs INCOMPATIBLE
	  Fixed lint-identified issues
	  Other minor bugfixes
 0.8: 2015-01-21
	  Fix dropdown-editing in row
 0.8.1: 2015-06-20
	  Fix event connection for jq > 1.7
 */
(function ($) {
  "$:nomunge";
  $.extend({
    tableDnD: new (function () {
      var vers = $.fn.jquery;
      // Class/public properties

      this.defaults = {
        buttonState: "", // Drag-button-index(ices) and/or modifier(s)
        containerID: null, //ID of DOM object containing the table. Must have style overflow: auto OR scroll. Default table-parent
        dragClass: "tDnD_whileDrag", // Class(es) added to dragged row during drag
        dragHandle: null, // Don't drag any row containing no cell(s) having this class
        dragStyles: { color: "purple", "font-style": "italic" }, // Map of CSS style(s) applied to a dragged row during drag
        dropStyles: null, // Map of CSS style(s) applied to a dragged row when dropped
        onAllowDrop: null, // Function called during drag to check whether dropping is allowed
        onDragStart: null, // Function called after drag starts
        onDrop: null, // Function called after drag ends
        onRowsChanged: null, // Function called during drag after rows changed
        filterRegexp: null, // Regular expression used to 'filter' rows (by ID etc) when mapping a table
      };

      // Class/public methods

      this.construct = function (options) {
        var common = {
          // Pointer to the table being dragged (or only clicked)
          currentTable: null,
          // Pointer to the tbody (a DOM element, not a jQuery object) containing the dragObject
          dragBody: null,
          // Array of draggable elements (each a table-row DOM element, not a jQuery object)
          dragObject: null,
          // Pointer-events-bound flag
          bound: false,
          // Pointer-drag-button pressed
          prepare: false,
          // Drag-in-progress flag
          dragging: false,
          // Pointer current-offset-object as {x,y}
          ptrOffset: null,
          // Pointer vert-position when drag-start-button was pressed
          firstY: 0,
          // Pointer previous vert-position (for incremental calculations)
          oldY: 0,
          // Auto-scroll-timer id's
          upid: 0,
          dnid: 0,
          // Whether using jquery >= 1.4, 1.7
          jqge14: false,
          jqge17: false,
          // Defaults and/or runtime options also merged into here
        };

        this.each(function () {
          var bodies = this.tBodies;
          // If no table body or not enough rows, quit
          if (!bodies) {
            return;
          }
          var rowcount = 0;
          for (var i = 0; i < bodies.length; i++) {
            rowcount += bodies[i].rows.length;
            if (rowcount > 1) {
              break;
            }
          }
          if (rowcount < 2) {
            return;
          }

          var cfg = {};
          $.extend(cfg, common, $.tableDnD.defaults, options || {});
          cfg.currentTable = this;
          // Make config data generally available
          $.data(this, "tDnDconfig", cfg);
          // Validate containerID
          var container;
          if (cfg.containerID) {
            container = $("#" + cfg.containerID);
            if (container.length > 0) {
              var style = container.css("overflow");
              if (!(style == "auto" || style == "scroll")) {
                cfg.containerID = null;
              }
            }
          }
          if (!cfg.containerID) {
            container = $(this).parent();
            style = container.css("overflow");
            if (style == "auto" || style == "scroll") {
              cfg.containerID = container.attr("id");
              if (!cfg.containerID) {
                cfg.containerID = "tDnDcontainerID";
                container.attr("id", cfg.containerID);
              }
            }
          }
          cfg.jqge17 = versionCompare(vers, "1.7") >= 0;
          cfg.jqge14 = cfg.jqge17 || versionCompare(vers, "1.4") >= 0;
          // Arrange to handle some pointer-events in table bodies
          if (cfg.jqge17) {
            $(this)
              .on("mouseover.tdnd", "tbody", cfg, ptrenter)
              .on("mouseout.tdnd", "tbody", cfg, ptrleave);
          } else {
            for (i = 0; i < bodies.length; i++) {
              $(bodies[i])
                .bind("mouseover.tdnd", cfg, ptrenter)
                .bind("mouseout.tdnd", cfg, ptrleave);
            }
          }
        });
        // The chain may continue ...
        return this;
      };

      this.mapTables = function () {
        var result = "";
        this.each(function () {
          result += map(this);
        });
        return result;
      };

      // Useful-in-callback object-methods

      // Get json-style version of simple-format table data
      function map(table) {
        var cfg = $.data(table, "tDnDconfig");
        var result = {};
        var rctr = 1;
        $(table)
          .find("tr")
          .each(function () {
            var rid =
              this.getAttribute("id") ||
              this.getAttribute("name") ||
              "row" + rctr;
            if (
              !cfg ||
              !cfg.filterRegexp ||
              rid.match(cfg.filterRegexp) !== null
            ) {
              var row = {};
              var ctr = 1;
              $(this).each(function () {
                var all = [];
                $(this)
                  .children()
                  .each(function () {
                    all[all.length] = this.hasAttribute("value")
                      ? this.getAttribute("value")
                      : $(this).text();
                  });
                var cid =
                  this.atgetAttribute("id") ||
                  this.getAttribute("name") ||
                  "col" + ctr;
                switch (all.length) {
                  case 0:
                    row[cid] = "<empty>";
                    break;
                  case 1:
                    row[cid] = all[0];
                    break;
                  default:
                    var rv = {};
                    for (var i = 0; i < all.length; ++i) rv[i] = all[i];
                    row[cid] = rv;
                    break;
                }
                ctr++;
              });
              result[rid] = row;
            }
            rctr++;
          });
        return result;
      }

      // Object/private methods

      // Get the pointer-device coordinates of an event
      function ptrCoords(ev) {
        if (ev.pageX || ev.pageY) {
          //browsers other than IE
          return { x: ev.pageX, y: ev.pageY };
        }
        var d =
          document.documentElement &&
          document.documentElement.scrollLeft !== null
            ? document.documentElement
            : document.body;
        return { x: ev.clientX + d.scrollLeft, y: ev.clientY + d.scrollTop };
      }

      /* Given a target element and a pointer event, get the pointer offset from that element.
		To do this we need the element's position and the pointer position */
      function getPtrOffset(target, ev) {
        ev = ev || window.event;
        var docPos = getPosition(target);
        var mousePos = ptrCoords(ev);
        return { x: mousePos.x - docPos.x, y: mousePos.y - docPos.y };
      }

      // Get position of element e, by going up the DOM tree and adding up all the offsets
      function getPosition(e) {
        var left = 0;
        var top = 0;
        if (e.offsetHeight === 0) {
          /* Safari 2, maybe 3, doesn't correctly grab the offsetTop of a table row
			this is detailed here:
			http://jacob.peargrove.com/blog/2006/technical/table-row-offsettop-bug-in-safari/
			the solution is likewise noted there, grab the offset of a table cell in the row - the firstChild.
			note that firefox will return a text node as a first child, so designing a more thorough
			solution may need to take that into account, for now this seems to work in firefox, safari, ie */
          if (e.firstChild) {
            e = e.firstChild; // a table cell
          }
        }

        while (e.offsetParent) {
          left += e.offsetLeft;
          top += e.offsetTop;
          e = e.offsetParent;
        }

        left += e.offsetLeft;
        top += e.offsetTop;

        return { x: left, y: top };
      }

      // Get vertical scroll of the current window
      function getScrollY() {
        var scroll = 0;
        if (window.pageYOffset) {
          scroll = window.pageYOffset;
        } else if (window.scrollY) {
          scroll = window.scrollY;
        } else {
          var t;
          scroll =
            ((t = document.documentElement) ||
              (t = document.body.parentNode)) &&
            typeof t.ScrollTop == "number"
              ? t.ScrollTop
              : document.body.ScrollTop;
        }
        return scroll;
      }

      function getScrollMaxY() {
        if (window.innerHeight && window.scrollMaxY) {
          // Firefox
          return window.scrollMaxY;
        }
        var innerh = window.innerHeight
          ? window.innerHeight
          : document.body.clientHeight;
        if (document.body.scrollHeight > document.body.offsetHeight) {
          // all but IE, Mac
          return document.body.scrollHeight - innerh;
        } else {
          // works in IE6 Strict, Mozilla (not FF), Safari
          return document.body.offsetHeight - innerh;
        }
      }

      /* Shift displayed part of table by small amount. Container is a scollable
	    DOM element. usewin (boolean) true = ignore container, scroll window instead
		upward (boolean) true = show more of table-top */
      function doScroll(container, usewin, upward, cfg) {
        var amount;
        var maxScroll;
        if (usewin) {
          var yOffset = getScrollY();
          if (upward) {
            //get scroll amount
            if (yOffset > 0) {
              amount = -1;
            } else {
              cfg.upid = 0;
              return;
            }
          } else {
            //get allowable scroll amount
            maxScroll = getScrollMaxY();
            if (yOffset < maxScroll) {
              amount = 1;
            } else {
              cfg.dnid = 0;
              return;
            }
          }
          window.scrollBy(0, amount);
        } else {
          amount = $(container).scrollTop();
          if (upward) {
            if (amount > 0) {
              amount -= 1;
            } else {
              cfg.upid = 0;
              return;
            }
          } else {
            maxScroll = container.scrollHeight - container.clientHeight;
            if (amount < maxScroll) {
              amount += 1;
            } else {
              cfg.dnid = 0;
              return;
            }
          }
          container.scrollTop(amount);
        }
        var t = setTimeout(function () {
          doScroll(container, usewin, upward, cfg);
        }, 20);
        if (upward) {
          cfg.upid = t;
        } else {
          cfg.dnid = t;
        }
      }

      // Cancel current auto-scrolling, if any
      function cancelScroll(cfg) {
        if (cfg.upid) {
          window.clearTimeout(cfg.upid);
          cfg.upid = 0;
        }
        if (cfg.dnid) {
          window.clearTimeout(cfg.dnid);
          cfg.dnid = 0;
        }
      }

      // Update display to show row is [to be] dragged
      function showDrag(row, cfg) {
        if (cfg.dropStyles) {
          var clear = {};
          for (var key in cfg.dropStyles) {
            clear[key] = "";
          }
          $(row).css(clear);
        }
        if (cfg.dragClass) {
          $(row).addClass(cfg.dragClass);
        }
        if (cfg.dragStyles) {
          $(row).css(cfg.dragStyles);
        }
      }

      // Update display to show row is not [to be] dragged
      function hideDrag(row, cfg) {
        if (cfg.dragStyles) {
          var clear = {};
          for (var key in cfg.dragStyles) {
            clear[key] = "";
          }
          $(row).css(clear);
        }
        if (cfg.dragClass) {
          $(row).removeClass(cfg.dragClass);
        }
      }

      /* Check whether ev is drag-related and was initiated on a draggable element
	    If so, return map with the element's tbody and tr ancestors, otherwise false */
      function isDraggable(ev) {
        var cfg = ev.data;
        if (cfg.buttonState) {
          var button;
          if (ev.which === null) {
            // some IE's
            button = ev.button < 2 ? "0" : ev.button === 4 ? "1" : "2";
          } else {
            // most other browsers
            button = ev.which < 2 ? "0" : ev.which === 2 ? "1" : "2";
          }
          if (cfg.buttonState.indexOf(button) === -1) {
            return false;
          }
          if (cfg.buttonState.indexOf("shift") !== -1 && !ev.shiftKey) {
            return false;
          }
          if (cfg.buttonState.indexOf("ctrl") !== -1 && !ev.ctrlKey) {
            return false;
          }
          if (cfg.buttonState.indexOf("alt") !== -1 && !ev.altKey) {
            return false;
          }
          if (cfg.buttonState.indexOf("meta") !== -1 && !ev.metaKey) {
            return false;
          }
        }
        var elem = ev.target || ev.srcElement || ev.originalTarget;
        var name = elem.nodeName.toLowerCase();
        switch (name) {
          case "td":
          case "tr":
            break;
          case "tbody":
          case "input":
          case "select":
          case "a":
            return false;
          default:
            elem = $(elem).closest("td");
            if (elem.length === 0) {
              return false;
            }
            name = "td";
            break;
        }

        var row;
        var body;
        if (cfg.dragHandle) {
          // We drag rows with specified cells
          var cells = $("td." + cfg.dragHandle, elem);
          if (cells.length > 0) {
            row = cells[0].parentNode;
            if (!$(row).hasClass("nodrag")) {
              body = $(row).parent("tbody")[0];
              if (body !== undefined) {
                return { tb: body, ob: row }; // NOT cells[0], we need the row
              }
            }
          }
        } else {
          // We drag rows regardless of content
          if (name == "td") {
            row = $(elem).parent("tr")[0];
            if (row === undefined) {
              return false;
            }
          } else {
            row = elem;
          }
          if (!$(row).hasClass("nodrag")) {
            body = $(row).parent("tbody")[0];
            if (body !== undefined) {
              return { tb: body, ob: row };
            }
          }
        }
        return false;
      }

      // Pointer-device-enter-tbody event handler
      function ptrenter(ev) {
        var cfg = ev.data;
        if (!cfg.draqgging) {
          if (cfg.jqge17) {
            $(this).on("mousemove.tdnd", cfg, pointermove);
          } else {
            $(this).bind("mousemove.tdnd", cfg, pointermove);
          }
        }
        return true;
      }

      // Pointer-device-leave-tbody event handler
      function ptrleave(ev) {
        var $this = $(this);
        var cfg = ev.data;
        if (!cfg.dragging) {
          cfg.currentTable.style.cursor = "default";
          if (cfg.jqge17) {
            $this.off("mousemove.tdnd", pointermove);
          } else {
            $this.unbind("mousemove.tdnd", pointermove);
          }
        }
        if (cfg.bound) {
          if (cfg.jqge17) {
            $this
              .off("mousedown.tdnd", btndown)
              .off("dblclick.tdnd", btnactivate);
          } else {
            $this
              .unbind("mousedown.tdnd", btndown)
              .unbind("dblclick.tdnd", btnactivate);
          }
          cfg.bound = false;
        }
        return true;
      }

      // Pointer-device-move-anywhere event handler
      function pointermove(ev) {
        var cfg = ev.data;
        var data = isDraggable(ev);
        if (!cfg.prepare) {
          if (data) {
            if (!cfg.bound) {
              cfg.currentTable.style.cursor = "crosshair";
              if (cfg.jqge17) {
                $(this)
                  .on("mousedown.tdnd", cfg, btndown)
                  .on("dblclick.tdnd", cfg, btnactivate);
                // Need jQuery 1.4+ for sorting when handling > 1 dragrow
              } else if (cfg.jqge14) {
                $(this)
                  .bind("mousedown.tdnd", cfg, btndown)
                  .bind("dblclick.tdnd", cfg, btnactivate);
              } else {
                $(this).bind("mousedown.tdnd", cfg, btndown);
              }
              cfg.bound = true;
            }
          } else if (cfg.bound && !cfg.dragging) {
            // Not dragging or draggable, but bound
            cfg.currentTable.style.cursor = "default";
            if (cfg.jqge17) {
              $(this)
                .off("mousedown.tdnd", btndown)
                .off("dblclick.tdnd", btnactivate);
            } else {
              $(this)
                .unbind("mousedown.tdnd", btndown)
                .unbind("dblclick.tdnd", btnactivate);
            }
            cfg.bound = false;
          }
          return true;
        }

        var mousePos = ptrCoords(ev);
        if (Math.abs(mousePos.y - cfg.firstY) <= 3) {
          return false; // Don't select text
        }

        // Begin drag if appropriate
        if (!cfg.dragging) {
          // Maybe add a(nother) row into the drag
          if (data) {
            if (cfg.dragBody) {
              if (cfg.dragBody != data.tb) {
                // Remove styling
                $.each(cfg.dragObject, function () {
                  hideDrag(this, cfg);
                });
                cfg.dragObject = null;
                cfg.dragBody = data.tb;
              }
            } else {
              cfg.dragBody = data.tb;
            }
            if (cfg.dragObject) {
              if ($.inArray(data.ob, cfg.dragObject) === -1) {
                cfg.dragObject[cfg.dragObject.length] = data.ob;
                showDrag(data.ob, cfg);
              }
            } else {
              cfg.dragObject = [data.ob];
              showDrag(data.ob, cfg);
            }
          }

          if (cfg.dragObject === null) {
            // Nothing draggable now
            btnup(ev); // Abort so we don't drag the next-traversed row
            return true;
          }

          $.unique(cfg.dragObject); // Sort in DOM order
          var ob = data ? data.ob : cfg.dragObject[0];
          cfg.ptrOffset = getPtrOffset(ob, ev);
          cfg.dragging = true;
          cfg.oldY = cfg.firstY - cfg.ptrOffset.y;
          // Call the start-handler if there is one
          if (cfg.onDragStart) {
            cfg.onDragStart(table, cfg.dragObject);
          }
          cfg.currentTable.style.cursor = "row-resize"; //vertical orientation
          return true;
        }

        var uprlimit;
        var btmlimit;
        var morescroll = false;
        // Preferably, auto-scroll the table-container if needed
        if (cfg.containerID) {
          var container = $("#" + cfg.containerID);
          uprlimit = container.offset().top;
          if (mousePos.y < uprlimit + 5) {
            if (cfg.upid === 0) {
              doScroll(container[0], false, true, cfg);
            }
            morescroll = true;
          } else {
            btmlimit = uprlimit + container.outerHeight(true);
            if (mousePos.y > btmlimit - 5) {
              if (cfg.dnid === 0) {
                doScroll(container[0], false, false, cfg);
              }
              morescroll = true;
            }
          }
        }
        // Otherwise, auto-scroll the window if needed
        if (!morescroll) {
          uprlimit = getScrollY(); // Vertical pixel-location of the top-left corner of the document
          if (mousePos.y < uprlimit + 5) {
            if (cfg.upid === 0) {
              doScroll(window, true, true, cfg);
            }
            morescroll = true;
          } else {
            btmlimit = uprlimit + $(window).height();
            if (mousePos.y > btmlimit - 5) {
              if (cfg.dnid === 0) {
                doScroll(window, true, false, cfg);
              }
              morescroll = true;
            }
          }
        }
        if (!morescroll) {
          cancelScroll(cfg);
        }

        var y = mousePos.y - cfg.ptrOffset.y;
        if (y != cfg.oldY) {
          var upward = y < cfg.oldY;
          // Check whether we're now over a droppable row
          var dropRow = null;
          var bodyrows = $("> tr", cfg.dragBody);
          var i;
          for (i = 0; i < bodyrows.length; i++) {
            var row = bodyrows[i];
            // We're only interested in vertical dimensions, because we only move rows up or down
            var rowY;
            var rowHeight;
            if (row.offsetHeight > 0) {
              rowY = getPosition(row).y;
              rowHeight = parseInt(row.offsetHeight, 10) / 2; // Dropzone limiter
            } else {
              rowY = getPosition(row.firstChild).y;
              rowHeight = parseInt(row.firstChild.offsetHeight, 10) / 2;
            }
            if (y > rowY - rowHeight && y < rowY + rowHeight) {
              // This one is the row we're over
              if ($.inArray(row, cfg.dragObject) === -1) {
                // No drops onto a dragged row
                if (cfg.onAllowDrop) {
                  if (cfg.onAllowDrop(cfg.dragObject, row)) {
                    dropRow = row;
                  }
                } else if (!$(row).hasClass("nodrop")) {
                  // Row has no 'blocking' class, allow the drop (inspired by John Tarr and Famic)
                  dropRow = row;
                }
              }
              break;
            }
          }
          // If we're over a droppable row, move the dragged row(s) to there,
          // so that the user sees the effect dynamically
          if (dropRow) {
            // Log fixed-position rows
            var fixed = [];
            $.each(bodyrows, function (indx) {
              if (
                $(this).hasClass("nodrop") &&
                $.inArray(this, cfg.dragObject) == -1
              ) {
                fixed[fixed.length] = [indx, this];
              }
            });
            if (upward) {
              // Dragging up
              $.each(cfg.dragObject, function () {
                if (this != dropRow) {
                  $(this).insertBefore(dropRow);
                } else {
                  var e = $(this).next();
                  if (e.length) {
                    dropRow = e[0];
                  }
                }
              });
            } else {
              // Down
              $.each(cfg.dragObject, function () {
                if (this != dropRow) {
                  $(this).insertAfter(dropRow);
                  dropRow = this;
                } else {
                  var e = $(this).prev();
                  if (e.length) {
                    dropRow = e[0];
                  }
                }
              });
            }
            // Reinstate fixed-position rows (which may have been moved up or down)
            var first = $("> tr", cfg.dragBody).eq(0);
            // 1 - accumulate fixed rows at start of table
            $.each(fixed, function () {
              if (this[1] != first[0]) {
                $(this[1]).insertBefore(first);
              }
            });
            // 2 - move fixed rows back
            var src = 0;
            for (var indx = 0; indx < fixed.length; indx++) {
              var pos = fixed[indx][0];
              if (pos > src) {
                var rows = $("> tr", cfg.dragBody);
                $(rows[src]).insertAfter($(rows[pos]));
              } else {
                src++;
              }
            }
            // Call the move-handler if there is one
            if (cfg.onRowsChanged) {
              cfg.onRowsChanged(this, cfg.dragObject, dropRow);
            }
          }
          // Update the old value
          cfg.oldY = y;
        }
        return false;
      }

      // Pointer-device-button-press-on-tbody event handler
      function btndown(ev) {
        var cfg = ev.data;
        cfg.prepare = true; // Initiate a drag, maybe
        cfg.firstY = ptrCoords(ev).y; // Remember where we started
        // Process button-releases anywhere, drags may end outside the table
        if (cfg.jqge17) {
          $(document).on("mouseup.tdnd", cfg, btnup);
        } else {
          $(document).bind("mouseup.tdnd", cfg, btnup);
        }
        return false; // Prevent text selection during start of drag
      }

      // Pointer-device-button-release-anywhere event handler (includes onDrop())
      function btnup(ev) {
        var cfg = ev.data;
        if (cfg.jqge17) {
          $(document).off("mouseup.tdnd", btnup);
        } else {
          $(document).unbind("mouseup.tdnd", btnup);
        }
        cfg.prepare = false;
        cfg.firstY = 0;
        if (cfg.dragging) {
          // This is after drag, not during a double-click
          if (cfg.dragObject) {
            // Row(s) will have been moved already, so we mainly reset stuff
            cancelScroll(cfg);
            cfg.oldY = 0;
            $.each(cfg.dragObject, function () {
              hideDrag(this, cfg);
            });
            if (cfg.dropStyles) {
              $.each(cfg.dragObject, function () {
                $(this).css(cfg.dropStyles);
              });
            }
            cfg.currentTable.style.cursor = "crosshair";
            // Call the drop-handler if there is one
            if (cfg.onDrop) {
              cfg.onDrop(cfg.currentTable, cfg.dragObject);
            }
            cfg.dragObject = null;
            cfg.dragBody = null;
          }
          cfg.dragging = false;
          return false;
        }
      }

      // Pointer-device-double-click-on-tbody event handler
      function btnactivate(ev) {
        var data = isDraggable(ev);
        if (!data) {
          return true;
        }
        var cfg = ev.data;
        if (cfg.dragBody) {
          if (cfg.dragBody != data.tb) {
            // Remove any styling
            $.each(cfg.dragObject, function () {
              hideDrag(this, cfg);
            });
            cfg.dragObject = null;
            cfg.dragBody = data.tb;
          }
        } else {
          cfg.dragBody = data.tb;
        }
        if (cfg.dragObject) {
          var at = $.inArray(data.ob, cfg.dragObject);
          if (at !== -1) {
            hideDrag(cfg.dragObject[at], cfg); // Remove styling
            if (cfg.dragObject.length == 1) {
              cfg.dragObject = null;
            } else {
              cfg.dragObject.splice(at, 1);
            }
          } else if (!$(data.ob).hasClass("nodrag")) {
            cfg.dragObject[cfg.dragObject.length] = data.ob;
            showDrag(data.ob, cfg); // Add styling
          }
        } else if (!$(data.ob).hasClass("nodrag")) {
          cfg.dragObject = [data.ob];
          showDrag(data.ob, cfg);
        }
        return false; // Prevent unwanted text selection
      }

      /*
       * @author Alexey Bass (albass)
       * @since 2011-07-14
       * Returns:
       * -1 if left is LOWER than right
       *  0 if they are equal
       *  1 if left is GREATER aka right is LOWER
       *  FALSE if left or right is not valid
       */
      versionCompare = function (left, right) {
        if (typeof left + typeof right != "stringstring") return false;

        var a = left.split("."),
          b = right.split("."),
          i = 0,
          len = Math.max(a.length, b.length);

        for (; i < len; i++) {
          if (
            (a[i] && !b[i] && parseInt(a[i]) > 0) ||
            parseInt(a[i]) > parseInt(b[i])
          ) {
            return 1;
          } else if (
            (b[i] && !a[i] && parseInt(b[i]) > 0) ||
            parseInt(a[i]) < parseInt(b[i])
          ) {
            return -1;
          }
        }
        return 0;
      };
    })(),
  });
  $.fn.extend({ tableDnD: $.tableDnD.construct });
})(jQuery);

var triggerCount = 0; GoalCheck = "", PageEventCheck = "", CampaignCheck = "", popupCheck = 0;

function triggerCampaign(campaignId, shouldTriggerCampaign, campaignData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { cid: campaignId, triggerCampaign: shouldTriggerCampaign, campaignData },
        context: this,
        success: function (data) {

        },
        error: function (data) {
            alert("Campaign is not been triggered", data);
        }
    });
}

function triggerGoal(goalId, shouldTriggerGoal, goalData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { gid: goalId, triggerGoal: shouldTriggerGoal, goalData },
        context: this,
        success: function (data) {

        },
        error: function (data) {
            alert("Goal is not been triggered", data);
        }
    });
}

function triggerPageEvent(pageEventId, shouldTriggerPageEvent, pageEventData) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { peid: pageEventId, triggerPageEvent: shouldTriggerPageEvent, pageEventData },
        context: this,
        success: function (data) {           


        },
        error: function (data) {
            alert("PageEvent is not been triggered", data);
        }
    });
}


function triggerGoal(goalId, shouldTriggerGoal) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { gid: goalId, triggerGoal: shouldTriggerGoal },
        context: this
    });
}

function triggerPageEvent(pageEventId, shouldTriggerPageEvent) {
    $.ajax({
        url: "/Events/Handler/TrackedLinkHandler.ashx",
        type: "GET",
        data: { peid: pageEventId, triggerPageEvent: shouldTriggerPageEvent },
        context: this
    });
}
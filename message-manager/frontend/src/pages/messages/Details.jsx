import api from "../../api";
import { useEffect, useState, forwardRef } from "react";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";
import { Box, Button, Heading, List, HStack, VStack, Input, Field, Fieldset, Textarea, Separator, Clipboard } from "@chakra-ui/react";
import Scripture from "./components/Scripture"

function formatTitle(title) {
  return title ?? "";
}

function formatDescription(description) {
  return description ?? "";
}

function formatSeries(series) {
  return series ?? "";
}

function formatDate(dateString) {
  if (dateString === null) return "";
  return new Date(dateString).toLocaleDateString('en-US',
    { year: 'numeric', month: 'long', day: 'numeric' });
}

function formatStartTime(seconds) {
  const t = new Date(1970, 0, 1); // Epoch
  t.setSeconds(seconds);
  return t.toTimeString().slice(0, 8);
}

function _StackSeparator() {
  return <Box height={10} />;
}

function ClipboardButton({ value }) {
  return (
    <Clipboard.Root value={value}>
      <Clipboard.Trigger asChild>
        <Button variant="surface" size="sm" height="100%">
          <Clipboard.Indicator />
        </Button>
      </Clipboard.Trigger>
    </Clipboard.Root>
  );
}

const CopyableTextBox = forwardRef(({ value, ...props }, ref) => {
  return (
    <HStack align="stretch" w="100%">
      <Textarea value={value} autoresize {...props} />
      <ClipboardButton value={value} />
    </HStack>
  );
});

function FieldsetHeading({ children }) {
  return (<>
    <Fieldset.Legend>
      <Heading size="lg" mb={1}>{children}</Heading>
    </Fieldset.Legend>
    <Separator m={0} />
  </>);
}

export function Details() {

  const [message, setMessage] = useState(null);
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    api.get(`/messages/${id}`).then((res) => setMessage(res.data));
  }, [id]);

  return (
    <Box p={6} maxW="800px" mx="auto">
      <Heading size="2xl" mb={8}>Message Details</Heading>
      <VStack gap="16px" separator={<_StackSeparator />}>

        {/* Basic Information */}
        <Fieldset.Root size="md">
          <FieldsetHeading>Basic Information</FieldsetHeading>
          <Field.Root mb={0} orientation="horizontal">
            <Field.Label>Title</Field.Label>
            <Input value={formatTitle(message?.title)} readOnly />
          </Field.Root>
          <Field.Root mb={0} orientation="horizontal">
            <Field.Label>Description</Field.Label>
            <Textarea value={formatDescription(message?.description)} readOnly autoresize />
          </Field.Root>
          <Field.Root mb={0} orientation="horizontal">
            <Field.Label>Date</Field.Label>
            <Input value={formatDate(message?.date)} readOnly />
          </Field.Root>
          <Field.Root mb={0} orientation="horizontal">
            <Field.Label>Series</Field.Label>
            <Input value={formatSeries(message?.series?.name)} readOnly />
          </Field.Root>
        </Fieldset.Root>

        {/* Scripture */}
        {message?.bibleReferences && message.bibleReferences.length > 0 && (
          <Fieldset.Root size="md">
            <FieldsetHeading>Scripture</FieldsetHeading>
            <List.Root>
              {message.bibleReferences.map((item, index) => (
                <List.Item key={item.id}>
                  <Scripture bibleReference={item} />
                </List.Item>
              ))}
            </List.Root>
          </Fieldset.Root >
        )}

        {/* Notes */}
        {(message?.notes) && (
          <Fieldset.Root size="md">
            <FieldsetHeading>Notes</FieldsetHeading>
            <Field.Root mb={0} orientation="horizontal">
              <Field.Label>URL</Field.Label>
              <CopyableTextBox value={message.notes.url} readOnly />
            </Field.Root>
          </Fieldset.Root>
        )}

        {/* Audio */}
        {(message?.audio) && (
          <Fieldset.Root size="md">
            <FieldsetHeading>Audio</FieldsetHeading>
            <Field.Root mb={0} orientation="horizontal">
              <Field.Label>Download URL</Field.Label>
              <CopyableTextBox value={message.audio.downloadUrl} readOnly />
            </Field.Root>
            <Field.Root mb={0} orientation="horizontal">
              <Field.Label>Stream URL</Field.Label>
              <CopyableTextBox value={message.audio.streamUrl} readOnly />
            </Field.Root>
          </Fieldset.Root>
        )}

        {/* Video */}
        {(message?.video) && (
          <Fieldset.Root size="md">
            <FieldsetHeading>Video</FieldsetHeading>
            <Field.Root mb={0} orientation="horizontal">
              <Field.Label>YouTube Video ID</Field.Label>
              <Input value={message.video.youTubeVideoId} readOnly />
            </Field.Root>
            <Field.Root mb={0} orientation="horizontal">
              <Field.Label>Start Time</Field.Label>
              <Input value={formatStartTime(message.video.messageStartTimeSeconds)} readOnly />
            </Field.Root>
          </Fieldset.Root>
        )}
      </VStack>
    </Box>
  );
}
